//Larry He

using UnityEngine;
using System.Collections;

public class GlassBreak : MonoBehaviour 
{
	public Transform brokenObject;
	public float magnitudeCol, radius, power, upwards;

	void OnCollisionEnter(Collision collision) {
		OnCollisionStay (collision);
	}

	void OnCollisionStay(Collision collision)
	{

		//if (collision.gameObject.tag == "Player" && (collision.relativeVelocity.magnitude > magnitudeCol || ((collision.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Dash")) && collision.relativeVelocity.magnitude > 6f)));
		if (collision.gameObject.tag == "Player" && collision.relativeVelocity.magnitude > magnitudeCol)
		{
			Destroy(gameObject);
			Instantiate(brokenObject, transform.position, transform.rotation);
			brokenObject.localScale = transform.localScale;
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);

			foreach (Collider hit in colliders)
			{
				if (hit.GetComponent<Rigidbody>())
				{
					hit.GetComponent<Rigidbody>().AddExplosionForce(power*collision.relativeVelocity.magnitude, explosionPos, radius, upwards);
				}
			}
		}
	}

	//totally good programming practice
	public void Break()
	{
		Debug.Log("Glass broken by bomb");
		Destroy(gameObject);
		Instantiate(brokenObject, transform.position, transform.rotation);
		brokenObject.localScale = transform.localScale;
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		
		foreach (Collider hit in colliders)
		{
			if (hit.GetComponent<Rigidbody>())
			{
				hit.GetComponent<Rigidbody>().AddExplosionForce(power*10f, explosionPos, radius, upwards);
			}
		}
	}
}
