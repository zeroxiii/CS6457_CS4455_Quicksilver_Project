// Team Quicksilver
// Rahmaan Lodhia

using UnityEngine;
using System.Collections;

public class SpikeTrapManager : MonoBehaviour 
{
	private RobotRagdoll ragdoll;
	private GameObject player;
	private GameObject deathPS;

	void Start () 
	{
		// Initialize variables
		player = GameObject.Find ("Player");
		ragdoll = player.GetComponent<RobotRagdoll>();
		deathPS = GameObject.Find ("DeathPS");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision collision) 
	{
		ParticleSystem ps = deathPS.GetComponent<ParticleSystem>();

		// If player collides with spikes, activate ragdoll and play death animation
		if (collision.gameObject.tag == "Player") 
		{
			ragdoll.activateRagdoll();
			ps.Play();
		}
	}
}
