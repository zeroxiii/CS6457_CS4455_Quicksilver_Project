// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class NewGameDoor : MonoBehaviour 
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
		GameObject door = GameObject.FindGameObjectWithTag("New Game");
		door.GetComponent<Animation>().Play("open");
		StartCoroutine(gm.GetComponent<GameManager>().ChangeLevel(1));
	}
}
