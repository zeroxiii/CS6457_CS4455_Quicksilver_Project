// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;

public class RobotParticleManager : MonoBehaviour 
{
	private GameObject leftThrusterPS;
	private GameObject rightThrusterPS;
	private GameObject shieldWavePS;
	private GameObject dashBangPS;
	private GameObject dashingSmokePS;
	private GameObject landingSmokePS;
	private GameObject wallCrashPS;
	private GameObject sizeChangePS;

	private RobotCharacter character; 
	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	private AnimatorStateInfo currentShieldState;

	void Start () 
	{
		anim = GetComponent<Animator>();
		character = GetComponent<RobotCharacter>();

		// Find all particle systems attached to player
		leftThrusterPS = GameObject.Find ("LeftThrusterPS");
		rightThrusterPS = GameObject.Find ("RightThrusterPS");
		shieldWavePS = GameObject.Find ("ShieldWavePS");
		dashBangPS = GameObject.Find ("DashBangPS");
		dashingSmokePS = GameObject.Find ("DashingSmokePS");
		landingSmokePS = GameObject.Find ("LandingSmokePS");
		wallCrashPS = GameObject.Find ("WallCrashPS");
		sizeChangePS = GameObject.Find ("SizeChangeAuraPS");

	}
	
	void Update () 
	{
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		currentShieldState = anim.GetCurrentAnimatorStateInfo(1);

		// Check if size change has been activated by the user
		if (character.sizeChangeTransition)
		{
			ActivateSizeChangeAura();
		}
		else if (!character.sizeChangeTransition)
		{
			DeativateSizeChangeAura();
		}

		// Check if character is holding shield
		if (currentShieldState.IsName("HoldShield") || currentBaseState.IsName("Dash"))
		{
			ActivateShieldWave();
		}
		else if (!currentShieldState.IsName("HoldShield") || !currentBaseState.IsName("Dash"))
		{
			DeactivateShieldWave();
		}

		// Check if character is dashing
		if (currentBaseState.IsName("Dash"))
		{
			ActivateDashEffect();
		}

	
	}

	public void ActivateSizeChangeAura()
	{
		ParticleSystem ps = sizeChangePS.GetComponent<ParticleSystem>();

		if (!ps.isPlaying)
		{
			ps.Play();
			sizeChangePS.GetComponent<AudioSource>().Play();
		}
	}

	public void DeativateSizeChangeAura()
	{
		ParticleSystem ps = sizeChangePS.GetComponent<ParticleSystem>();
		
		if (ps.isPlaying)
		{
			ps.Stop();
		}
	}

	public void ActivateShieldWave()
	{
		ParticleSystem ps = shieldWavePS.GetComponent<ParticleSystem>();
		
		if (!ps.isPlaying)
		{
			ps.Play();
		}
	}

	public void DeactivateShieldWave()
	{
		ParticleSystem ps = shieldWavePS.GetComponent<ParticleSystem>();
		
		if (ps.isPlaying)
		{
			ps.Stop();
			ps.Clear();
		}
	}

	public void ActivateDashEffect()
	{
		ParticleSystem ps = dashBangPS.GetComponent<ParticleSystem>();
		
		if (!ps.isPlaying)
		{
			ps.Play();
			dashBangPS.GetComponent<AudioSource>().Play();
		}
	}

	public void ActivateLandingSmokeEffect()
	{
		ParticleSystem ps = landingSmokePS.GetComponent<ParticleSystem>();
		
		if (!ps.isPlaying)
		{
			ps.Play();
			landingSmokePS.GetComponent<AudioSource>().Play();
		}
	}

	public void ActivateWallCrashEffect()
	{
		ParticleSystem ps = wallCrashPS.GetComponent<ParticleSystem>();
		
		if (!ps.isPlaying)
		{
			ps.Play();
			wallCrashPS.GetComponent<AudioSource>().Play();
		}
	}
}
