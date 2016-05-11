using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class gameOverScript : MonoBehaviour {

	public static gameOverScript Instance;
	public GameObject gameOverGameObject;
	public Text scoreText;
	public Button doubleScoreButton;
	public enemyGenerator enemygeneratorScript;
	public Text gameOverText;
	public Text retryButtonText;
	public int coinsToWinLevel = 3000;
	int wave = 0;
	int coins = 0;
	void Awake() {
		Instance = this;
		SpaceAdsDemo.OnCoinsAddedAction += SetCoinAmount;
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnDestroy ()
	{
		SpaceAdsDemo.OnCoinsAddedAction -= SetCoinAmount;
	}

	public void SetCoinAmount (int amount)
	{
		coins = Inventory.GetCoinCount ();
		setScoreText ();
	}

	public void gameOver(){
		gameOverGameObject.SetActive (true);
		gameOverText.text = "GAME OVER";
		retryButtonText.text = "RETRY";
		coins = Inventory.GetCoinCount ();
		if (Advertisement.IsReady ("rewardedVideo")) {
			doubleScoreButton.interactable = true;	
		}
		CheckForLevelComplete ();
	}

	public void CheckForLevelComplete() {
		setScoreText ();
		if (coins >= coinsToWinLevel) {
			levelFinished ();
		}
	}

	public void levelFinished(){
		gameOverText.text = "Level complete!";
		retryButtonText.text = "Next level";
	}

	public void setScoreText() {
		wave = 1;
		if (coins > 100) {
			int scoreNeeded = 100;
			while (coins > scoreNeeded) {
				scoreNeeded *= 2;
				wave++;
			}
		}
		scoreText.text = "Score: " + coins.ToString() + "/" + coinsToWinLevel.ToString() + "\n" + 
			"Wave: " + wave.ToString();
	}

	public void retryButton(){
		if (coins < coinsToWinLevel) {
			enemygeneratorScript.setWave (wave);
			enemyGenerator.enemiesAlive = 0;
			enemyGenerator.spawning = true;
			Inventory.ResetCoinCount ();
			SceneManager.LoadScene ("Gameplay");
		} else {
			SceneManager.LoadScene ("GameplayDemo");
		}
	}

	public void doubleScore() {
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		doubleScoreButton.interactable = false;
		Advertisement.Show ("rewardedVideo", options);
	}


	private void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Inventory.AddCoins (Inventory.GetCoinCount ());
			coins = Inventory.GetCoinCount ();
			setScoreText ();
			CheckForLevelComplete ();
			break;
		case ShowResult.Skipped:
			Debug.LogWarning ("Video was skipped.");
			break;
		case ShowResult.Failed:
			Debug.LogError ("Video failed to show.");
			break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
