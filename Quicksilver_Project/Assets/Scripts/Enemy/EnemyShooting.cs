// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff


using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour 
{
	public Vector3 targetPosition;
	public GameObject particleBlast;

	LineRenderer attackLine;
	Animator anim;
	bool attackReady;

	// Use this for initialization
	void Start () 
	{
		attackLine = GetComponent<LineRenderer>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && attackReady)
		{
			Shoot ();
			attackReady = false;
		}
		else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			DisableEffects ();
			attackReady = true;
		}
	}

	public void DisableEffects()
	{
		attackLine.enabled = false;
	}

	void Shoot ()
	{
//		attackLine.enabled = true;
//		attackLine.SetPosition (0, transform.position + new Vector3(0f, 2.5f, 0f));
//		attackLine.SetPosition (1, targetPosition);
		particleBlast.transform.LookAt(GameObject.Find ("Player").transform.position + new Vector3(0, 1.5f, 0));
		ParticleSystem ps = particleBlast.GetComponent<ParticleSystem>();
		ps.Clear();
		ps.Simulate(0.0001f, true, true);
		ps.Play();
		particleBlast.GetComponent<AudioSource>().Play();

	}
}
