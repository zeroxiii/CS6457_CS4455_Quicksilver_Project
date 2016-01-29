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

[RequireComponent(typeof (RobotCharacter))]
[RequireComponent(typeof (RobotRagdoll))]

public class RobotUserControl : MonoBehaviour
{
    private RobotCharacter character; 
	private RobotRagdoll ragdoll;
	private IsometricCamera isoCam;
    private Transform cam;                  
    private Vector3 camForward;             
    private Vector3 move;
    private bool jump;                      
	private bool dash;
	private bool crouch;
	private bool attack;
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
		isoCam = Camera.main.GetComponent<IsometricCamera>();
		crouch = false;
		guard = false;
		leftTriggerReleased = true;
		rightTriggerReleased = true;
    }


    private void Update()
    {
		// On each update check which buttons are pressed to determine if an action is being requested
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }

		if (!dash)
		{
			dash = Input.GetButtonDown("Dash");
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = !crouch;
		}

		if (Input.GetButtonDown("Attack"))
		{
			attack = true;
		}

		if (Input.GetButtonDown("SizeChange"))
		{
			sizeChange = true;
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

		guard = Input.GetButton("Guard");
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
        	character.Move(move, crouch, jump, dash, guard, attack, sizeChange);
		}
		// Reset all input booleans, so that the action is ready to be performed again
        jump = false;
		dash = false;
		sizeChange = false;
		attack = false;
    }
}