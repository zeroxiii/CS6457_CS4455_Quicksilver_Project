// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class RobotProjectileManager : MonoBehaviour 
{
	public int damageAmount;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnParticleCollision (GameObject other)
	{
		if (other.tag == "Enemy")
		{
			Debug.Log ("Enemy Hit");
			other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
		}

		if (other.tag == "Bomb")
		{
			Debug.Log ("Bomb Hit");
			other.GetComponent<BombExplode>().Explode();
		}
	}
}
