using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bonusScript : MonoBehaviour {

	public Text amount;
	public Text description;
	Transform m_trans;

	void Awake() {
		m_trans = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (shrinkAndDestroy ());
	}

	IEnumerator shrinkAndDestroy() {
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < 100f; i++) {
			yield return null;
			m_trans.localScale *= 0.95f;
		}
		Destroy (gameObject);
	}
}
