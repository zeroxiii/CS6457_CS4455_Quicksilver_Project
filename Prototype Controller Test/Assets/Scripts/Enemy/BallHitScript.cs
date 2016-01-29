using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BallHitScript : MonoBehaviour {
	public AudioClip impact;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision other) 
	{
		if(other.gameObject.tag == "Player")
		{
			audio.PlayOneShot(impact);
		}
		
	}
}

