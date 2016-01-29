// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class Kill : MonoBehaviour 
{	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<RobotEnergy>().TakeDamage (100);
		}
	}
}
