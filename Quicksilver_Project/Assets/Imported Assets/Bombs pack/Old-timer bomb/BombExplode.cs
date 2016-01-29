using UnityEngine;
using System.Collections;

public class BombExplode : MonoBehaviour 
{
	public Transform explodeObject;
	public float magnitudeCol, radius, power, upwards;
	
	public void Explode()
	{
		Destroy (gameObject);
		Instantiate (explodeObject, transform.position, transform.rotation);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			Explode ();
		}
	}
}
