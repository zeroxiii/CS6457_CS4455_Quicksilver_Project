// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;
using ProgressBar;

public class GUIManager : MonoBehaviour 
{

	public GameObject ShrinkMeterGUI;
	public GameObject dashButtonGUI;
	public GameObject attackButtonGUI;
	public GameObject shootButtonGUI;
	public GameObject jumpButtonGUI;

	public int shrinkProgressSpeed;
	public float dashProgressSpeed;
	public float attackProgressSpeed;
	public float shootProgressSpeed;
	public float jumpProgressSpeed;

	public ProgressBarBehaviour shrinkMeter;
	public ProgressRadialBehaviour dashButton;
	public ProgressRadialBehaviour attackButton;
	public ProgressRadialBehaviour shootButton;
	public ProgressRadialBehaviour jumpButton;

	private bool jumpAvailable = true;


	// Use this for initialization
	void Start () 
	{

		dashButton = dashButtonGUI.GetComponentInChildren<ProgressRadialBehaviour>();
		dashButton.ProgressSpeed = dashProgressSpeed;
		dashButton.IncrementValue(100f);

		attackButton = attackButtonGUI.GetComponentInChildren<ProgressRadialBehaviour>();
		attackButton.ProgressSpeed = attackProgressSpeed;
		attackButton.IncrementValue(100f);

		shootButton = shootButtonGUI.GetComponentInChildren<ProgressRadialBehaviour>();
		shootButton.ProgressSpeed = shootProgressSpeed;
		shootButton.IncrementValue(100f);

		jumpButton = jumpButtonGUI.GetComponentInChildren<ProgressRadialBehaviour>();
		jumpButton.ProgressSpeed = jumpProgressSpeed;
		jumpButton.IncrementValue(100f);

		shrinkMeter = ShrinkMeterGUI.GetComponent<ProgressBarBehaviour>();
		shrinkMeter.ProgressSpeed = shrinkProgressSpeed;
		shrinkMeter.IncrementValue(100f);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shootButton.Value == 0f && shootButton.isPaused)
		{
			shootButton.ProgressSpeed = shootProgressSpeed;
			shootButton.IncrementValue(100f);
		}

		if (attackButton.Value == 0f && attackButton.isPaused)
		{
			attackButton.ProgressSpeed = attackProgressSpeed;
			attackButton.IncrementValue(100f);
		}

		if (jumpButton.TransitoryValue == 0f && jumpAvailable)
		{	
			jumpAvailable = false;
		}

		if (!jumpAvailable && jumpButton.TransitoryValue == 1f)
		{
			jumpAvailable = true;
		}
	}

	public void StartAttackCooldown()
	{
		attackButton.ProgressSpeed = 3.0f;
		attackButton.Value = 0.0f;
	}

	public void StartShootCooldown()
	{
		shootButton.ProgressSpeed = 2.0f;
		shootButton.Value = 0.0f;
	}

	public void StartDashTimer()
	{
		dashButton.ProgressSpeed = 1.5f;
		dashButton.DecrementValue(100f); 
	}

	public void StartDashRecharge()
	{
		dashButton.ProgressSpeed = dashProgressSpeed;
		dashButton.IncrementValue(100f);
	}

	public void StartJumpRecharge()
	{
		jumpButton.ProgressSpeed = jumpProgressSpeed;
		jumpButton.IncrementValue(100f);
	}

	public void StartJumpTimer()
	{
		jumpButton.ProgressSpeed = 1.5f;
		jumpButton.DecrementValue(100f);
	}

	public void StartShrinkTimer()
	{
		shrinkMeter.DecrementValue(100f);
	}

	public void StartShrinkRecharge()
	{
		shrinkMeter.IncrementValue(100f);
	}

	public bool IsShrinkReady()
	{
		return (shrinkMeter.isDone && shrinkMeter.isPaused);
	}

	public bool IsShrinkOver()
	{
		return (shrinkMeter.isPaused && shrinkMeter.Value == 0.0f);
	}

	public bool IsDashOver()
	{
		return (dashButton.isPaused && dashButton.Value == 0.0f);
	}

	public bool IsJumpOver()
	{
		return (jumpButton.isPaused && jumpButton.Value == 0.0f);
	}

	public bool IsAttackReady()
	{
		return (attackButton.isDone && attackButton.isPaused);
	}

	public bool IsShootReady()
	{
		return (shootButton.isDone && shootButton.isPaused);
	}

	public bool IsDashReady()
	{
		return (dashButton.isDone && dashButton.isPaused);
	}

	public bool IsJumpReady()
	{
		return (jumpButton.TransitoryValue > 0.0f && jumpAvailable);
	}
}
