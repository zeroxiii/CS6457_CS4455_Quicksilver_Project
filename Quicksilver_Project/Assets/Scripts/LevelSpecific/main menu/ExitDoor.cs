// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour 
{
	private GameObject gm;
	// Use this for initialization
	void Start () 
	{
		gm = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter (Collider other)
	{
		GameObject door = GameObject.FindGameObjectWithTag("Exit");
		//door.GetComponent<Animation>().Play("open");
		gm.GetComponent<GameManager>().ExitGame();
	}

	void OnTriggerExit (Collider other)
	{
		GameObject door = GameObject.FindGameObjectWithTag("Exit");
		//door.GetComponent<Animation>().Play("close");
	}
}
