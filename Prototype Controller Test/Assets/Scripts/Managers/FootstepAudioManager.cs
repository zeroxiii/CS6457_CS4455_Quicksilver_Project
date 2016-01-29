// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class FootstepAudioManager : MonoBehaviour 
{
	// Varibles to use
	public GameObject player;
	private Animator anim;
	private AudioSource[] footstep;
	private bool step = true;
	private bool rightStep = true;
	private bool leftStep = true;
	private bool stepTransition = true;
	private float lastLeg = 0.0f;

	void Start () 
	{
		// Get access to player animator and audio sources
		anim = player.GetComponent<Animator>();
		footstep = GetComponentsInChildren<AudioSource>();
	}
	
	void Update () 
	{
	
	}
	
	void OnTriggerStay(Collider other)
	{
		// Check if player has entered a footstep trigger
		if (other.gameObject.tag == "Player")
		{
			// Determine if transition is occuring between steps
			float currentLeg = anim.GetFloat("JumpLeg");
			if (currentLeg > 0 && lastLeg < 0)
				stepTransition = true;
			else if (currentLeg < 0 && lastLeg > 0)
				stepTransition = true;
			else if (currentLeg != 0 && lastLeg == 0)
				stepTransition = true;
			else
				stepTransition = false;

			// Check which foot needs a sound played and play function accordingly
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
		rightStep = false;
		footstep[0].Play();
		yield return new WaitForSeconds (0.8f);
		rightStep = true;
	}

	
	IEnumerator WaitForNextRightRunStep()
	{
		// Plays a run step and waits ample time before the next on
		rightStep = false;
		footstep[0].Play();
		yield return new WaitForSeconds (0.4f);
		rightStep = true;
	}

	IEnumerator WaitForNextLeftWalkStep()
	{
		// Plays a walk step and waits ample time before the next on
		leftStep = false;
		footstep[1].Play();
		yield return new WaitForSeconds (0.8f);
		leftStep = true;
	}

	IEnumerator WaitForNextLeftRunStep()
	{
		// Plays a run step and waits ample time before the next on
		leftStep = false;
		footstep[1].Play();
		yield return new WaitForSeconds (0.4f);
		leftStep = true;
	}
}