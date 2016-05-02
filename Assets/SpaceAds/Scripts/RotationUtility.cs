using UnityEngine;
using System.Collections;

public class RotationUtility : MonoBehaviour 
{
	public enum AxisOfRotation { X, Y, Z }
	public AxisOfRotation axisOfRotation;
	public float degreesPerSecond;
	private Transform m_transform;

	void Awake() {
		m_transform = GetComponent<Transform> ();
	}

	void Update ()
	{
		if (degreesPerSecond != 0) UpdateRotation(degreesPerSecond);
	}
		
	private void UpdateRotation (float degreesPerSecond)
	{
		Vector3 rotation = m_transform.localRotation.eulerAngles;
		Vector3 direction = Vector3.zero;

		switch (axisOfRotation)
		{
		case AxisOfRotation.X:
			direction = Vector3.right;
			break;
		case AxisOfRotation.Y:
			direction = Vector3.up;
			break;
		case AxisOfRotation.Z:
			direction = Vector3.forward;
			break;
		}

		rotation += direction * degreesPerSecond * Time.deltaTime;
		
		m_transform.localRotation = Quaternion.Euler(rotation);
	}
}
