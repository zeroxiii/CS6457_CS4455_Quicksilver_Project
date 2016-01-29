// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour 
{
	public int damageAmount;
	public GameObject enemyParent;

	private Animator anim;
	private bool attackReady;

	// Use this for initialization
	void Start () 
	{
		damageAmount = damageAmount;
		anim = enemyParent.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			attackReady = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && attackReady)
		{
			other.gameObject.GetComponent<RobotEnergy>().TakeDamage(damageAmount);
			attackReady = false;
		}
	}
}
