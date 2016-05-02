using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour {

	public float hitPoints = 100f;
	public Image ufoImage;
	public Color endColor;
	public GameObject explosion;
	public Collider enemyCollider;
	private float maxHitPoints;
	private Color startColor;
	private bool destroying = false;
	private Transform m_transform;
	private Rigidbody m_rigid;
	private bool hasBeenHit = false;
	private MeshRenderer meshrenderer;
	private float centeringSpeed = 300f;

	void Awake() {
		m_transform = GetComponent<Transform> ();
		m_rigid = GetComponent<Rigidbody> ();
		meshrenderer = GetComponent<MeshRenderer> ();
	}

	// Use this for initialization
	void Start () {
		maxHitPoints = hitPoints;
		startColor = ufoImage.color;
		//StartCoroutine (checkForPosition ());
	}

	void OnEnable() {
		StartCoroutine (checkForPosition ());
	}

	void OnBecameVisible() {
		enemyCollider.enabled = true;
		meshrenderer.enabled = false;
	}

	IEnumerator stabilize() {
		yield return new WaitForSeconds (0.5f);
		m_rigid.angularVelocity = Vector3.zero;
		m_transform.rotation = Quaternion.identity;
	}

	IEnumerator checkForPosition(){
		while (true) {
			yield return new WaitForSeconds (1f);
			if (Mathf.Abs(m_transform.position.y) > 12) {
				reset ();
				enemyGenerator.enemiesAlive--;
				gameObject.SetActive (false);
			}
			if (m_transform.position.x < -15f) {
				reset ();
				enemyGenerator.enemiesAlive--;
				gameObject.SetActive (false);
			}
		}
	}

	void reset() {
		hitPoints = 100f;
		destroying = false;
		m_rigid.angularVelocity = Vector3.zero;
		m_rigid.velocity = Vector3.zero;
		hasBeenHit = false;
		m_rigid.mass = 100f;
		ufoImage.color = startColor;
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.layer == 8) {
			if (hasBeenHit) {
				gotHit (100f);
				enemyScript hitEnemy = coll.gameObject.GetComponent<enemyScript> ();
				hitEnemy.gotHit (100f);
			}
		}
		if (coll.gameObject.tag == "Player") {
			Instantiate (explosion, m_transform.position, Quaternion.identity);
			Instantiate (explosion, coll.transform.position, Quaternion.identity);
			gotHit (100f, true);
			enemyGenerator.spawning = false;
			gameOverScript.Instance.gameOver ();
			Destroy (coll.gameObject);
		}
	}

	IEnumerator destroyInSeconds(float seconds) {
		yield return new WaitForSeconds (seconds-0.1f);
		Instantiate (explosion, m_transform.position, Quaternion.identity);
		yield return new WaitForSeconds (0.1f);
		bonusManagerScript.current.addToSpree (m_transform.position);
		reset ();
		enemyGenerator.enemiesAlive--;
		gameObject.SetActive (false);
	}

	public void gotHit(float damage, bool wasHitByLaser = false)
	{
		if (!destroying) {
			if (wasHitByLaser)
				hasBeenHit = true;
			hitPoints -= damage;

			ufoImage.color = Color.Lerp (endColor, startColor, (hitPoints / maxHitPoints));
			if (hitPoints <= 0) {
				destroying = true;
				m_rigid.mass = 20f;
				StartCoroutine (destroyInSeconds (0.5f));
			}
		}
	}

	void FixedUpdate() {
		// If the enemy is way ahead, move it closer
		if (m_transform.position.x > 12) {
			m_transform.position -= Vector3.right * Time.deltaTime * 5f;
			Vector3 newVelocity = m_rigid.velocity;
			newVelocity.x = Mathf.Clamp (newVelocity.x, -1000f, 0f);
			m_rigid.velocity = newVelocity;
		}

		// Move the enemy towards the horizontal center of the screen
		if (m_transform.position.y > 0) {
			m_rigid.AddForce (-Vector3.up * centeringSpeed);
			if (m_transform.position.y > 7) {
				m_transform.position -= Vector3.up*Time.deltaTime;
			}
		} else {
			m_rigid.AddForce (Vector3.up * centeringSpeed);
			if (m_transform.position.y < -7) {
				m_transform.position += Vector3.up*Time.deltaTime;
			}

		}
	}
}
