// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;
using RAIN.Core;

public class EnemyHealth : MonoBehaviour 
{
	
	public int Health;
	public int EnergyReward;
	
	private RobotEnergy playerEnergy;
	private Animator anim;
	private AIRig ai;
	private int startingHealth;
	private CapsuleCollider col;
	public GameObject DamageSmokePS;
	private bool IsDead = false;
	private bool invincilbe = false;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		ai = GetComponentInChildren<AIRig>();
		col = GetComponent<CapsuleCollider>();
		startingHealth = Health;
		playerEnergy = GameObject.Find("Player").GetComponent<RobotEnergy>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ParticleSystem ps = DamageSmokePS.GetComponent<ParticleSystem>();
		//Debug.Log (Health);
		
		if (Health <= startingHealth/2 && !ps.isPlaying)
		{
			ps.Play();
		}
		
		if (Health <= 0 && !IsDead)
		{
			Die ();
		}
		
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("TurnOffAnimator"))
		{
			anim.enabled = false;
			ps.Stop();
		}

		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
			invincilbe = false;
		}
	}
	
	public void TakeDamage (int amount)
	{
		if (!invincilbe)
		{
			Debug.Log ("Take Damage");
			Health = Health - amount;
			anim.SetFloat("Speed", 0f);
			anim.SetTrigger("HitTrigger");
			invincilbe = true;
		}
	}
	
	public void Die ()
	{
		if (this.gameObject.name.Contains ("Regen Warrior")) 
		{
			Debug.Log("I live!");
			playerEnergy.IncreaseEnergy(EnergyReward);
			Health = 5;
			EnergyReward = 0;
			ParticleSystem ps = DamageSmokePS.GetComponent<ParticleSystem>();
			ps.Stop();
			anim.SetBool("Death", true);
			return;
		}
		IsDead = true;
		anim.SetBool("Death", true);
		ai.enabled = false;
		col.enabled = false;
		DamageSmokePS.transform.position = this.transform.position;
		DamageSmokePS.GetComponent<AudioSource>().Play();
		playerEnergy.IncreaseEnergy(EnergyReward);
	}
}
