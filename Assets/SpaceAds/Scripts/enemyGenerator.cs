using UnityEngine;
using System.Collections;

public class enemyGenerator : MonoBehaviour {

	public GameObject enemy;
	public GameObject[] enemyPool;
	public static bool spawning = true;
	public static int wave = 1;
	int currentEnemy = 0;
	public static int enemiesAlive = 0;
	int amountToSpawn = 5;
	Transform m_transform;

	void Awake() {
		m_transform = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		spawning = true;
		enemiesAlive = 0;
		for (int i = 0; i < enemyPool.Length; i++) {
			Debug.Log ("Instantiating enemy");
			enemyPool [i] = Instantiate (enemy, Vector3.left * 200f, Quaternion.identity) as GameObject;
			enemyPool [i].SetActive (false);
		}
		if (PlayerPrefs.HasKey ("Wave")) {
			setWave (PlayerPrefs.GetInt ("Wave"));
		} else {
			setWave (1);
		}
		StartCoroutine (spawnNewEnemies ());
	}

	public void setWave(int newWave) {
		wave = newWave;
		PlayerPrefs.SetInt ("Wave", wave);
		amountToSpawn = 5;
		shootLaserScript.beamForceMultiplier = 1f;
		for (int i = 1; i < wave; i++) {
			amountToSpawn = Mathf.Clamp (amountToSpawn + 2, 0, enemyPool.Length);
			shootLaserScript.beamForceMultiplier *= 1.1f;
		}
	}

	IEnumerator spawnNewEnemies() {
		yield return new WaitForSeconds (2f);
		while (true) {
			while (enemiesAlive > wave || !spawning) {
				yield return new WaitForSeconds (1f);
			}
			setWave (wave);
			for (int i = 0; i < amountToSpawn; i++) {
				enemyPool [currentEnemy].GetComponent<Transform> ().position = 
					m_transform.position + 
					new Vector3 (Random.Range (0, 20f + amountToSpawn), Random.Range (-4f, 4f), m_transform.position.z);
				enemyPool [currentEnemy].SetActive (true);
				Vector3 newConstantForce = enemyPool [currentEnemy].GetComponent<ConstantForce> ().force;
				newConstantForce.x *= 1.1f;
				enemyPool [currentEnemy].GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-10f, 0f), 0f, 0f);
				enemyPool [currentEnemy].GetComponent<ConstantForce> ().force = newConstantForce;
				currentEnemy = (currentEnemy + 1) % enemyPool.Length;
			}
			enemiesAlive = amountToSpawn;
			yield return new WaitForSeconds (1f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
