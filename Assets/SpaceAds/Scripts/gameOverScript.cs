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
	int wave = 0;
	int coins = 0;
	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void gameOver(){
		gameOverGameObject.SetActive (true);
		coins = Inventory.GetCoinCount ();
		if (Advertisement.IsReady ("rewardedVideo")) {
			doubleScoreButton.interactable = true;	
		}
		setScoreText ();
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
		scoreText.text = "Score: " + coins.ToString() + "\n" + 
			"Wave: " + wave.ToString();
	}

	public void retryButton(){
		enemygeneratorScript.setWave (wave);
		enemyGenerator.enemiesAlive = 0;
		enemyGenerator.spawning = true;
		Inventory.ResetCoinCount ();
		SceneManager.LoadScene ("Gameplay");
	}

	public void doubleScore() {
		doubleScoreButton.interactable = false;
		Advertisement.Show ("rewardedVideo");
		coins *= 2;
		setScoreText ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
