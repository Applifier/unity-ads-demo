using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class readyGoScript : MonoBehaviour {

	public Text readyGoText;

	// Use this for initialization
	void Start () {
		StartCoroutine (showReadyGo ());
	}

	IEnumerator showReadyGo() {
		readyGoText.text = "Wave " + enemyGenerator.wave.ToString ();
		yield return new WaitForSeconds (2f);
		readyGoText.text = "GO!";
		yield return new WaitForSeconds (1.5f);
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
