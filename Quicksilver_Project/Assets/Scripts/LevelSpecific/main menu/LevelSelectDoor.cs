// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class LevelSelectDoor : MonoBehaviour 
{

	private GameObject gm;
	private bool menuOpen = false;
	
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
		GameObject door = GameObject.FindGameObjectWithTag("Level Select");
		door.GetComponent<Animation>().Play("open");
		if (!menuOpen)
		{
			menuOpen = true;
			gm.GetComponent<GameManager>().OpenLevelSelectMenu();
		}
	}

	void OnTriggerExit (Collider other)
	{
		GameObject door = GameObject.FindGameObjectWithTag("Level Select");
		door.GetComponent<Animation>().Play("close");
	}
	
}
