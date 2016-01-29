// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

// Built from the ThirdPersonUserControl.cs from Unity Standard Assets
// Some base functionality used
// Heavily modified with several additional code added to create the best controller for character

using System;
using UnityEngine;
using RAIN.Entities;

[RequireComponent(typeof (RobotCharacter))]
[RequireComponent(typeof (RobotRagdoll))]
[RequireComponent(typeof (RobotParticleManager))]

public class RobotUserControl : MonoBehaviour
{
    private RobotCharacter character; 
	private RobotRagdoll ragdoll;
	private RobotParticleManager particle;
	private RobotEnergy energy;
	private EntityRig entity;
	private GameManager GM;
	private GUIManager gui;
	private IsometricCamera isoCam;
    private Transform cam;                  
    private Vector3 camForward;             
    private Vector3 move;
    private bool jump;                      
	private bool dash;
	private bool crouch;
	private bool attack;
	private bool shoot;
	private bool guard;
	private bool sizeChange;
	private bool leftTriggerReleased;
	private bool rightTriggerReleased;
	private int cameraPos = 0;

    private void Start()
    {
        // Get the transform of the main camera to allow for camera-relative controls
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
        }

        // Initialize variables
        character = GetComponent<RobotCharacter>();
		ragdoll = GetComponent<RobotRagdoll>();
		particle = GetComponent<RobotParticleManager>();
		energy = GetComponent<RobotEnergy>();
		isoCam = Camera.main.GetComponent<IsometricCamera>();
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
		gui = GameObject.Find ("GUI").GetComponentInChildren<GUIManager>();
		entity = GetComponentInChildren<EntityRig>();
		crouch = false;
		guard = false;
		leftTriggerReleased = true;
		rightTriggerReleased = true;
		energy.StartEnergyDrain();
    }


    private void Update()
    {
		if (gui.IsShrinkOver() && !character.normalSizedState)
		{
			sizeChange = true;
			gui.StartShrinkRecharge();
			entity.Entity.ActivateEntity();
		}

		if (gui.IsDashOver())
		{
			dash = false;
			gui.StartDashRecharge();
		}

		if (gui.IsJumpOver())
		{
			jump = false;
			gui.StartJumpRecharge();
		}

		// On each update check which buttons are pressed to determine if an action is being requested
		if (Input.GetButton("Jump") && gui.IsJumpReady())
		{
			jump = true;
			gui.StartJumpTimer();
			particle.ActivateThrusterEffect();
		}
		else if (!Input.GetButton("Jump"))
		{
			jump = false;
			gui.StartJumpRecharge();
			particle.DeactivateThrusterEffect();
		}

		if (Input.GetButton("Dash") && gui.IsDashReady() && character.isGrounded)
		{
			dash = true;
			gui.StartDashTimer();
			energy.DashEnergyCost();
		}
		else if (!Input.GetButton("Dash"))
		{
			dash = false;
			gui.StartDashRecharge();
		}

//		if (Input.GetButtonDown("Crouch"))
//		{
//			crouch = !crouch;
//		}

		if (Input.GetButtonDown("Attack") && gui.IsAttackReady())
		{
			attack = true;
			gui.StartAttackCooldown();
			energy.AttackEnergyCost();
		}

		if (Input.GetButtonDown ("Shoot") && gui.IsShootReady())
		{
			shoot = true;
			gui.StartShootCooldown();
			energy.ShootEnergyCost();
		}

		if (Input.GetButtonDown("SizeChange"))
		{
			if (gui.IsShrinkReady())
			{
				gui.StartShrinkTimer();
				sizeChange = true;
				energy.ShrinkEnergyCost();
				entity.Entity.DeactivateEntity();
			}
			else if (!character.normalSizedState)
			{
				gui.StartShrinkRecharge();
				sizeChange = true;
				entity.Entity.ActivateEntity();
			}
		}

		if (Input.GetButtonDown ("Death")) 
		{
			if (ragdoll.isRagdollActive)
			{
				ragdoll.deactivateRagdoll();
			}
			else
			{
				ragdoll.activateRagdoll();
			}
		}

		if (Input.GetAxis ("CameraLeft") == 1 && leftTriggerReleased)
		{
			leftTriggerReleased = false;
			cameraPos -= 1;
			if (cameraPos < 0)
			{
				cameraPos = 3;
			}
			else if (cameraPos > 3)
			{
				cameraPos = 0;
			}
			isoCam.RotateCamera(cameraPos);
			Debug.Log ("LeftTrigger Pressed");
		}
		else if (Input.GetAxis ("CameraLeft") < 1)
		{
			leftTriggerReleased = true;
		}

		if (Input.GetAxis ("CameraRight") == 1 && rightTriggerReleased)
		{
			rightTriggerReleased = false;
			cameraPos += 1;
			if (cameraPos < 0)
			{
				cameraPos = 3;
			}
			else if (cameraPos > 3)
			{
				cameraPos = 0;
			}
			isoCam.RotateCamera(cameraPos);
			Debug.Log ("RightTrigger Pressed");
		}
		else if (Input.GetAxis("CameraRight") < 1)
		{
			rightTriggerReleased = true;
		}

		if (Input.GetButton("Guard") && !guard)
		{
			guard = true;
			energy.StartShieldDrain();
			entity.Entity.DeactivateEntity();
		}
		else if (!Input.GetButton("Guard") && guard)
		{
			guard = false;
			energy.StopShieldDrain();
			entity.Entity.ActivateEntity();
		}
    }
	
    private void FixedUpdate()
    {
        // Read movement inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

		// Calculate move direction to pass to character
        if (cam != null)
        {
            // Calculate camera relative direction to move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = v*camForward + h*cam.right;
        }
        else
        {
            // Use world-relative directions in the case of no main camera
            move = v*Vector3.forward + h*Vector3.right;
        }

        // Pass all parameters to the character control script as long ragdoll is not active
		if (!ragdoll.isRagdollActive)
		{
        	character.Move(move, crouch, jump, dash, guard, attack, shoot, sizeChange);
		}
		// Reset all input booleans, so that the action is ready to be performed again
		sizeChange = false;
		attack = false;
		shoot = false;
    }
}