// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class RobotColliderManager : MonoBehaviour 
{

	private Animator anim;
	private RobotParticleManager psManager;

	private AnimatorStateInfo currentBaseState;

	private bool pastGroundState;
	private bool currentGroundState;
	private bool hasEnteredAir;

	void Start () 
	{
		// Initialize variables
		anim = GetComponent<Animator>();
		psManager = GetComponent<RobotParticleManager>();
		hasEnteredAir = false;
		pastGroundState = true;
	}
	
	void Update ()
	{
		// Check animator to if character has entered the air
		currentGroundState = anim.GetBool("OnGround");
		if (!currentGroundState && pastGroundState)
		{
			hasEnteredAir = true;
		}
	}

	void OnCollisionEnter (Collision collision) 
	{
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		// Check if character has collided with wall
		if (collision.gameObject.tag == "Wall") 
		{
			// Check if character has hit wall while dashing
			if (currentBaseState.IsName("Dash"))
			{
				psManager.ActivateWallCrashEffect();
			}
		}

		// Check if character has landed on floor from a fall state
		if (collision.gameObject.tag == "Floor" && hasEnteredAir)
		{
			psManager.ActivateLandingSmokeEffect();
			hasEnteredAir = false;
		}
	}

}
