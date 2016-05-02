using UnityEngine;
using System.Collections;

public class movePlayerScript : MonoBehaviour {

	Transform m_trans;
	Vector3 targetPosition;
	Resolution res;

	void Awake() {
		m_trans = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		res = Screen.currentResolution;
		setTargetPosition ();
	}
		
	void setTargetPosition() {
		targetPosition = Camera.main.ScreenToWorldPoint (new Vector3 (res.width*0.05f, res.height*0.2f, 0f));
		targetPosition.z = 0f;
		targetPosition.y = 0f;
	}

	// Update is called once per frame
	void Update () {
		m_trans.position = Vector3.Lerp (m_trans.position, targetPosition, Time.deltaTime);
		if (res.width != Screen.currentResolution.width || res.height != Screen.currentResolution.height) {
			res = Screen.currentResolution;
			setTargetPosition ();
		}
	}
}
