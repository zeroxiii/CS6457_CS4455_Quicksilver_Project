// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

	public GameObject pauseMenuPanel;
	public GameObject resultsScreen;
	public GameObject levelSelectMenuPanel;
	public GameObject energyBar;

	public float targetScore;
	private float currentScore;
	private int totalLevels;

	public bool gamePaused = false;
	public bool levelSelect = false;
	
	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;

		totalLevels = Application.levelCount;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Start") && !levelSelect)
		{
			if (!gamePaused)
			{
				PauseGame();
				if (pauseMenuPanel != null)
				{
					OpenPauseMenu();
				}
			}
			else if (gamePaused)
			{
				ResumeGame();
				if (pauseMenuPanel != null)
				{
					ClosePauseMenu();
				}
			}
		}

		if (energyBar != null)
		{
			currentScore = energyBar.GetComponent<GUIBarScript>().Value * 100;
		}
	}
	
	public void PauseGame ()
	{
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		gamePaused = true;
	}

	public void ResumeGame ()
	{
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		gamePaused = false;
	}

	public void OpenPauseMenu ()
	{
		pauseMenuPanel.SetActive (true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find ("Resume"));
	}

	public void ClosePauseMenu ()
	{
		pauseMenuPanel.SetActive (false);
	}

	public void OpenLevelSelectMenu ()
	{
		levelSelect = true;
		PauseGame();
		levelSelectMenuPanel.SetActive (true);
		EventSystem.current.SetSelectedGameObject(GameObject.Find ("Level 1"));
	}
	
	public void CloseLevelSelectMenu ()
	{
		levelSelect = false;
		ResumeGame();
		levelSelectMenuPanel.SetActive (false);
	}

	public void ReturnToMainMenu ()
	{
		//EventSystem.current.SetSelectedGameObject(GameObject.Find ("ExitToMainMenu"));
		ResumeGame ();
		StartCoroutine(ChangeLevel(0));
	}

	public void RestartLevel ()
	{
		//EventSystem.current.SetSelectedGameObject(GameObject.Find ("Restart"));
		ResumeGame ();
		StartCoroutine(ChangeLevel(Application.loadedLevel));
	}

	public void LoadLevel1 ()
	{
		ResumeGame ();
		StartCoroutine (ChangeLevel(1));
	}

	public void LoadLevel (int index)
	{
		ResumeGame ();
		StartCoroutine (ChangeLevel(index));
	}

	public void LoadNextLevel ()
	{
		int targetIndex = Application.loadedLevel + 1;
		ResumeGame ();
		StartCoroutine (ChangeLevel(targetIndex));
	}

	public void OpenSuccessScreen ()
	{
		resultsScreen.SetActive (true);
		PauseGame();
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		currentScore = energyBar.GetComponent<GUIBarScript>().Value * 100;
		GameObject.Find ("Target_Text").GetComponent<Text>().text = "Target Score: " + targetScore;
		GameObject.Find ("Actual_Text").GetComponent<Text>().text = "Current Score: " + currentScore;
		energyBar.SetActive(false);

		if (currentScore >= targetScore)
		{
			GameObject.Find ("Result_Text").GetComponent<Text>().text = "Result: Success";
			GameObject.Find ("Results_Next").SetActive(true);
			EventSystem.current.SetSelectedGameObject(GameObject.Find ("Results_Next"));

		}
		else
		{
			GameObject.Find ("Result_Text").GetComponent<Text>().text = "Result: Failure";
			GameObject.Find ("Results_Next").SetActive(false);
			EventSystem.current.SetSelectedGameObject(GameObject.Find ("Results_Restart"));

		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public IEnumerator ChangeLevel (int index)
	{
		Debug.Log("fading");
		float fadetime = this.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadetime);
		Application.LoadLevel(index);
	}
}
