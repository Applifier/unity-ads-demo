using UnityEngine;
using System.Collections;

public class explosionScript : MonoBehaviour {

	public float delay = 5f;
	Transform m_trans;


	void Awake() {
		m_trans = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (destroyAfter (delay));
		pushEnemies ();
	}

	public void pushEnemies() {
		Collider[] colliders = Physics.OverlapSphere (m_trans.position, 2f);
		foreach (Collider coll in colliders) {
			if (coll.enabled) {

				Rigidbody targetRigid = coll.GetComponent<Rigidbody> ();
				if (targetRigid != null) {
					targetRigid.AddExplosionForce (5000f, m_trans.position, 3f);
				}
				enemyScript targetEnemy = coll.gameObject.GetComponent<enemyScript> ();
				if (targetEnemy != null) {
					targetEnemy.gotHit (20f, true);
				}
			}
		}
	}

	IEnumerator destroyAfter(float seconds){
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
