// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class SceneSwitchManager : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check if button has been input and switch to respective scene
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			Application.LoadLevel("lodhia_r_m2");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)|| Input.GetKeyDown(KeyCode.Keypad2))
		{
			Application.LoadLevel ("gu_w_m2");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)|| Input.GetKeyDown(KeyCode.Keypad3))
		{
			Application.LoadLevel ("he_l_m2");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4)|| Input.GetKeyDown(KeyCode.Keypad4))
		{
			Application.LoadLevel ("leff_m_m2");
		}
	}
}