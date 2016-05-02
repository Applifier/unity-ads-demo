using UnityEngine;
using System.Collections;

public class bonusManagerScript : MonoBehaviour {

	public GameObject bonusFlash;
	public static bonusManagerScript current;
	int destroyed = 0;
	float destroyedSince = 0f;
	Vector3 lastDestroyedPosition;
	void Awake() {
		bonusManagerScript.current = this;
		lastDestroyedPosition = Vector3.zero;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (createBonuses ());
	}

	IEnumerator createBonuses() {
		while (true) {
			yield return null;
			while (destroyed > 0) {
				destroyedSince += Time.deltaTime;
				if (destroyedSince >= 0.5f) {
					if (destroyed > 1) {
						GameObject newBonusFlash = GameObject.Instantiate (bonusFlash, lastDestroyedPosition, Quaternion.identity) as GameObject;
						bonusScript newBonusScript = newBonusFlash.GetComponent<bonusScript> ();
						int newBonusAmount = (5 + (5 * destroyed)) * destroyed;
						newBonusScript.amount.text = "+" + newBonusAmount.ToString ();
						newBonusScript.description.text = destroyed.ToString () + "x combo";	
						Inventory.AddCoins (newBonusAmount);
					}
					Inventory.AddCoins (10);
					destroyed = 0;
					destroyedSince = 0f;
					
				}
				yield return null;
			}
		}
	}

	public void addToSpree(Vector3 destructionPosition) {
		destroyed++;
		destroyedSince = 0f;
		lastDestroyedPosition = Vector3.Lerp(lastDestroyedPosition, destructionPosition,0.5f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
