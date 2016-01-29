// Team Quicksilver
// Rahmaan Lodhia

using UnityEngine;
using System.Collections;

public class GravelFootstepManager : MonoBehaviour 
{

	public GameObject player;
	private Animator anim;
	private AudioSource[] footstep;
	private GameObject dirtPS;

	private bool step = true;
	private bool rightStep = true;
	private bool leftStep = true;
	private bool stepTransition = true;
	private float lastLeg = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		// Get access to player animator and audio sources
		anim = player.GetComponent<Animator>();
		footstep = GetComponentsInChildren<AudioSource>();
		dirtPS = GameObject.Find ("DirtTrailPS");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerStay(Collider other)
	{

		ParticleSystem ps = dirtPS.GetComponent<ParticleSystem>();

		// Check if player has entered a footstep trigger
		if (other.gameObject.tag == "Player")
		{
			float currentLeg = anim.GetFloat("JumpLeg");
			if (currentLeg > 0 && lastLeg < 0)
				stepTransition = true;
			else if (currentLeg < 0 && lastLeg > 0)
				stepTransition = true;
			else if (currentLeg != 0 && lastLeg == 0)
				stepTransition = true;
			else
				stepTransition = false;
			
			if(anim.GetFloat("JumpLeg") != 0)
			{
				if (anim.GetFloat("JumpLeg") > 0 && rightStep && stepTransition)
				{
					if (anim.GetFloat("JumpLeg") < 0.8)
					{
						StartCoroutine(WaitForNextRightWalkStep());
					}
					else
					{
						StartCoroutine(WaitForNextRightRunStep());
					}
				}
				if (anim.GetFloat("JumpLeg") < 0 && leftStep && stepTransition)
				{
					if (anim.GetFloat("JumpLeg") > -0.8)
					{
						StartCoroutine(WaitForNextLeftWalkStep());
					}
					else
					{
						StartCoroutine(WaitForNextLeftRunStep());
					}
				}
			}
			lastLeg = currentLeg;
		}
	}
	
	IEnumerator WaitForNextRightWalkStep()
	{
		// Plays a walk step and waits ample time before the next on
		ParticleSystem ps = dirtPS.GetComponent<ParticleSystem>();
		rightStep = false;
		ps.Play();
		footstep[0].Play();
		yield return new WaitForSeconds (0.8f);
		rightStep = true;
	}
	
	
	IEnumerator WaitForNextRightRunStep()
	{
		// Plays a run step and waits ample time before the next on
		ParticleSystem ps = dirtPS.GetComponent<ParticleSystem>();
		rightStep = false;
		ps.Play();
		footstep[0].Play();
		yield return new WaitForSeconds (0.4f);
		rightStep = true;
	}
	
	IEnumerator WaitForNextLeftWalkStep()
	{
		// Plays a walk step and waits ample time before the next on
		ParticleSystem ps = dirtPS.GetComponent<ParticleSystem>();
		leftStep = false;
		ps.Play();
		footstep[1].Play();
		yield return new WaitForSeconds (0.8f);
		leftStep = true;
	}
	
	IEnumerator WaitForNextLeftRunStep()
	{
		// Plays a run step and waits ample time before the next on
		ParticleSystem ps = dirtPS.GetComponent<ParticleSystem>();
		leftStep = false;
		ps.Play();
		footstep[1].Play();
		yield return new WaitForSeconds (0.4f);
		leftStep = true;
	}
}