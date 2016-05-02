using UnityEngine;
using System.Collections;

public class shootLaserScript : MonoBehaviour {

	public GameObject beamGlow;
	public LineRenderer particleBeam;
	public LineRenderer particleBeamSpreads;
	public Material beamSpreadMaterial;
	public Transform beamGun;
	public float beamForce = 200f;
	public static float beamForceMultiplier = 1f;
	Transform m_transform;
	Transform m_beamGlow;
	Light m_beamLight;
	ParticleSystem m_beamParticles;
	ParticleSystem.EmissionModule em;
	bool shooting = false;
	float m_distance;


	void Awake() {
		m_transform = GetComponent<Transform> ();
		particleBeam.enabled = false;
		particleBeamSpreads.enabled = false;
		m_beamGlow = beamGlow.GetComponent<Transform> ();
		m_beamLight = beamGlow.GetComponent<Light> ();
		m_beamParticles = beamGlow.GetComponent<ParticleSystem> ();
		em = m_beamParticles.emission;
	}

	void OnDestroy() {
		if (m_beamGlow && m_beamGlow.gameObject) {
			m_beamGlow.gameObject.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		beamForceMultiplier = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			shooting = true;
			particleBeam.enabled = true;
			particleBeamSpreads.enabled = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			shooting = false;
			particleBeam.enabled = false;
			particleBeamSpreads.enabled = false;
			m_beamLight.enabled = false;
			em.enabled = false;
		}
		if (shooting) {
			RaycastHit hit;
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//Debug.Log ("World point : " + worldPoint);
			worldPoint.z = beamGun.position.z;

			if (Physics.Raycast (beamGun.position, worldPoint-beamGun.position, out hit, 17f)) {
				particleBeam.SetPosition (0, beamGun.position);
				particleBeamSpreads.SetPosition (0, beamGun.position);
				particleBeam.SetPosition (1, hit.point);
				particleBeamSpreads.SetPosition (1, hit.point);
				//float m_fDistance = Vector3.Distance(, m_vTargetPos);
				m_distance = hit.distance;
				beamSpreadMaterial.SetTextureOffset("_MainTex", new Vector2(Random.Range(0f, 1.0f), 0));
				beamSpreadMaterial.SetTextureScale("_MainTex", new Vector2(m_distance / 4f, 1f));
				m_beamGlow.position = hit.point;
				m_beamLight.enabled = true;
				em.enabled = true;
				//Debug.Log ("HIT");
				GameObject targetGameObject = hit.collider.gameObject;
				Rigidbody targetRigid = targetGameObject.GetComponent<Rigidbody> ();
				if (targetRigid) {
					targetRigid.AddExplosionForce (beamForce * beamForceMultiplier, hit.point, 10f);
					enemyScript enemy = targetGameObject.GetComponent<enemyScript> ();
					enemy.gotHit (3f, true);
				}

			} else {
				Vector3 targetPosition = (worldPoint - beamGun.position).normalized * 100f;

				particleBeam.SetPosition (0, beamGun.position);
				particleBeamSpreads.SetPosition (0, beamGun.position);
				particleBeam.SetPosition (1, targetPosition);
				particleBeamSpreads.SetPosition (1, targetPosition);
				m_distance = 100f;
				beamSpreadMaterial.SetTextureOffset("_MainTex", new Vector2(Random.Range(0f, 1.0f), 0));
				beamSpreadMaterial.SetTextureScale("_MainTex", new Vector2(m_distance / 4f, 1f));
				m_beamGlow.position = targetPosition;
				em.enabled = false;
			}

		}
	}
}
