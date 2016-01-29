// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class RobotRagdoll : MonoBehaviour 
{
	public Transform ragdollModel;

	public Component[] bones;
	public Animator anim;
	public Collider col;
	public bool isRagdollActive;


	void Start () 
	{
		// Initialize variables and set ragdoll to false
		bones = ragdollModel.gameObject.GetComponentsInChildren<Rigidbody>();
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		isRagdollActive = false;
	}
	
	void Update () 
	{
	
	}
	
	public void activateRagdoll ()
	{
		// To activate ragdool, each ragdoll bone must be enabled
		foreach (Rigidbody ragdoll in bones)
		{
				ragdoll.isKinematic = false;
		}
		anim.enabled = false;
		isRagdollActive = true;
	}

	public void deactivateRagdoll ()
	{	
		// To deactivate ragdoll, each ragdoll bone must be disabled
		foreach (Rigidbody ragdoll in bones)
		{
				ragdoll.isKinematic = true;
		}
		anim.enabled = true;
		isRagdollActive = false;
	}
}
