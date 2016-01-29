// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour 
{
	public Transform target;
	public float distanceX = 45.0f;
	public float distanceZ = 45.0f;
	public float distanceY = 45.0f;
	public float distanceDamping = 1.0f;
	public float heightDamping = 1.0f;
	private float[] cameraXPos = {45.0f, 45.0f, -45.0f, -45.0f};
	private float[] cameraZPos = {45.0f, -45.0f, -45.0f, 45.0f};

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void LateUpdate ()
	{
		float wantedDistanceX = target.position.x + distanceX;
		float wantedDistanceY = target.position.y + distanceY;
		float wantedDistanceZ = target.position.z - distanceZ;
		float currentDistanceX = transform.position.x;
		float currentDistanceY = transform.position.y;
		float currentDistanceZ = transform.position.z;

		currentDistanceX = Mathf.Lerp (currentDistanceX, wantedDistanceX, distanceDamping * Time.deltaTime);
		currentDistanceY = Mathf.Lerp (currentDistanceY, wantedDistanceY, heightDamping * Time.deltaTime);
		currentDistanceZ = Mathf.Lerp (currentDistanceZ, wantedDistanceZ, distanceDamping * Time.deltaTime);

		transform.position = new Vector3(currentDistanceX, currentDistanceY, currentDistanceZ);

		transform.LookAt(target);
	}

	public void RotateCamera(int cameraPos)
	{
		distanceX = cameraXPos[cameraPos];
		distanceZ = cameraZPos[cameraPos];
	}
}
