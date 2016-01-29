// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public float smooth = 3f;		// A public variable to adjust smoothing of camera motion
	Transform standardPos;			// The usual position for the camera, specified by a transform in the game
	public Transform target;
	Vector3 offset;


	void Start()
	{
		// Initialize references
		//standardPos = GameObject.Find("robot_m2/CamPos").transform;
		offset = transform.position - target.position;
	}
	
	void FixedUpdate ()
	{
		// Moves the camera smoothly to follow the character
		//transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
		//transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smooth * Time.deltaTime);
	}
}
