using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour {
	
	bool isPickedUp = false;
	float time = .5f;
	float startY;

	// Use this for initialization
	void Start () {
		startY = transform.position.y;
	}

	void FixedUpdate() {
		transform.position = new Vector3(transform.position.x, startY + .35f * Mathf.Sin (3.5f*Time.time), transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		if (isPickedUp) {
			time -= Time.deltaTime;
		}
		if (time < 0f) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player" && !isPickedUp) {
			collider.gameObject.GetComponent<RobotEnergy>().AddEnergy(10);
			Destroy(gameObject.GetComponent<MeshRenderer>());
			Destroy(gameObject.GetComponent<ParticleSystem>());
			GetComponent<AudioSource>().Play ();
			isPickedUp = true;
		}
	}
}
