// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

// Built from the ThirdPersonCharacter.cs from Unity Standard Assets
// Some base functionality used
// Heavily modified with several additional code added to create the best controller for character

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class RobotCharacter: MonoBehaviour
{
	// Animation and collider parameters
	[SerializeField] float movingTurnSpeed = 360;
	[SerializeField] float stationaryTurnSpeed = 180;
	[Range(1f, 4f)][SerializeField] float gravityMultiplier = 2f;
	[SerializeField] float runCycleLegOffset = 0.2f; 
	[SerializeField] float groundCheckDistance = 0.2f;

	// Movement variables
	private float dashingSpeed = 10;
	private float jumpPower = 12f;
	private float moveSpeedMultiplier = 1f;
	private float animSpeedMultiplier = 1f;

	// Variables to indicate how normal-sized character should move
	[SerializeField] float normalDashingSpeed = 10;
	[SerializeField] float normalJumpPower = 5f;
	[SerializeField] float normalMoveSpeedMultiplier = 1f;
	[SerializeField] float normalAnimSpeedMultiplier = 1f;
	[SerializeField] float normalMass = 60;

	// Variables to indicate how shrunken-sized character should move
	[SerializeField] float shrunkDashingSpeed = 20;
	[SerializeField] float shrunkJumpPower = 15f;
	[SerializeField] float shrunkMoveSpeedMultipler = 2f;
	[SerializeField] float shrunkAnimSpeedMultiplier = 1f;
	[SerializeField] float shrunkMass = 1;

	// Variables for use of character size changes
	[SerializeField] float targetShrinkScale = 0.5f;
	[SerializeField] float sizeChangeSpeed = 5f;
	public bool sizeChangeTransition;
	public bool normalSizedState;
	private bool shrinkingActivated;
	private bool growingActivated;

	// Variables to hold current state of Animator layers
	private AnimatorStateInfo currentBaseState;
	private AnimatorStateInfo currentShieldState;
	private AnimatorStateInfo currentAttackState;

	// Variables used to reference other components and set current states of character
	private Rigidbody rig;
	private Animator anim;
	public bool isGrounded;
	private bool isDashing;
	private bool isGuarding;
	private bool isAirDashing;
	private float origGroundCheckDistance;
	const float k_Half = 0.5f;
	private float turnAmount;
	private float forwardAmount;
	private Vector3 groundNormal;
	private float capsuleHeight;
	private Vector3 capsuleCenter;
	private CapsuleCollider col;
	private GameObject shieldCol;
	private bool isCrouching;


	void Start()
	{
		// Initialize Variables
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		shieldCol = GameObject.Find ("ShieldCollider");
		capsuleHeight = col.height;
		capsuleCenter = col.center;

		rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		origGroundCheckDistance = groundCheckDistance;

		dashingSpeed = normalDashingSpeed;
		jumpPower = normalJumpPower;
		moveSpeedMultiplier = normalMoveSpeedMultiplier;
		animSpeedMultiplier = normalAnimSpeedMultiplier;

		sizeChangeTransition = false;
		normalSizedState = true;
	}

	private void Update()
	{
		// Check if sizeChange is currently requested and apply the proper scaling update
		if (shrinkingActivated)
		{
			Shrink();
		}
		else if (growingActivated)
		{
			Grow();
		}
	}

	public void Move(Vector3 move, bool crouch, bool jump, bool dash, bool guard, bool attack, bool shoot, bool sizeChange)
	{
		// Convert move vector to a direction and turn amount
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, groundNormal);
		turnAmount = Mathf.Atan2(move.x, move.z);
		forwardAmount = move.z;

		// Get current animation state of each layer
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		currentShieldState = anim.GetCurrentAnimatorStateInfo(1);
		currentAttackState = anim.GetCurrentAnimatorStateInfo(2);

		// Check if a size change is requested and if so, determine which direction to scale
		if (sizeChange && !sizeChangeTransition)
		{
			if (normalSizedState)
			{
				shrinkingActivated = true;
			}
			else
			{
				growingActivated = true;
			}
			sizeChangeTransition = true;
		}

		// Check to see we are dashing in order to lock out some movement during dashing
		if (!currentBaseState.IsName("Dash"))
		{
			ApplyExtraTurnRotation();
			if(!currentBaseState.IsName("Crouching"))
			{
				if (attack)
				{
					anim.SetBool("Attack", true);
				}
				else if (shoot)
				{
					anim.SetBool("Shoot", true);
				}
			}

			// Determine if guarding is allowed
			if (guard && currentBaseState.IsName("Grounded"))
			{
				isGuarding = guard;
			}
			else
			{
				isGuarding = false;
			}
		}
		else
		{
			isGuarding = false;
		}

		// Animate shield collider using animation curves
		if (currentBaseState.IsName("Dash"))
		{
			shieldCol.transform.localScale = Vector3.one * anim.GetFloat("ShieldColliderScale");
		}

		if (currentShieldState.IsName("RaiseShield") || currentShieldState.IsName("LowerShield"))
		{
			shieldCol.transform.localScale = Vector3.one * anim.GetFloat("ShieldColliderScale");
		}


		// Control and velocity handling is different when grounded and airborne:
		if (isGrounded)
		{
			HandleGroundedMovement(crouch, jump, dash);
		}
		else
		{
			HandleAirborneMovement(move, dash);
			if (jump)
			{
				HandleVerticleDashMovement();
			}
		}

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();

		// Send input and other state parameters to the animator
		UpdateAnimator(move);
	}

	void CheckGroundStatus()
	{
		// Create layer mask to ignore certain objects in scene
		int layerMask = (1 << 8) + (1 << 9) + (1 << 10) + (1 << 11) + (1 << 12) + (1 << 2);
		layerMask = ~layerMask;
		RaycastHit hitInfo;
		float sphereRadiusScale = (normalSizedState == true) ? 1f : targetShrinkScale;

		#if UNITY_EDITOR
		// Helper function to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
		#endif

		// Casts a ray down to check ground status
//		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance, layerMask))
//		{
//			groundNormal = hitInfo.normal;
//			isGrounded = true;
//			anim.applyRootMotion = true;
//		}
//		else
//		{
//			isGrounded = false;
//			groundNormal = Vector3.up;
//			anim.applyRootMotion = false;
//		}
		if (Physics.SphereCast(transform.position + (Vector3.up * 0.6f), 0.3f, Vector3.down, out hitInfo, groundCheckDistance + 0.6f, layerMask))
		{
			groundNormal = hitInfo.normal;
			isGrounded = true;
			anim.applyRootMotion = true;
		}
		else
		{
			isGrounded = false;
			groundNormal = Vector3.up;
			anim.applyRootMotion = false;
		}

	}

	void ApplyExtraTurnRotation()
	{
		// Help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void HandleGroundedMovement(bool crouch, bool jump, bool dash)
	{
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		int layerMask = (1 << 9) + (1 << 10) + (1 << 11) + (1 << 2);
		layerMask = ~layerMask;

		// Check whether conditions are right to allow a jump:
		if (jump && !crouch && currentBaseState.IsName("Grounded"))
		{
			rig.velocity = new Vector3(0, jumpPower, 0);
			isGrounded = false;
			anim.applyRootMotion = false;
			groundCheckDistance = 0.1f;
		}

		// Check wheter conditions are right to allow a ground dash
		if (currentBaseState.IsName("Grounded"))
		{
			if (dash)
			{
				anim.SetBool("Dash",true);
			}
		}
		else if (currentBaseState.IsName("Dash") )
		{
			HandleDashMovement(dash);
		}
	}

	void HandleAirborneMovement(Vector3 move, bool dash)
	{
		// Create a layerMask to avoid specified layers
		int layerMask = (1 << 9) + (1 << 10) + (1 << 11) + (1 << 2);
		layerMask = ~layerMask;
		
		// Check to see if air dashing is requested and conditions are met for it otherwise continue falling while airborne
		if (dash && !normalSizedState)
		{
			//anim.SetBool("Dash",true);
			//anim.SetFloat("Jump", 0f);
			//isAirDashing = true;
		}
		else if (currentBaseState.IsName("Dash"))
		{
			//HandleDashMovement(dash);
		}
		else
		{
			// Apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
			rig.AddForce(extraGravityForce);

			 //After reaching peak of jump, allow character to alter falling path
			if (forwardAmount > 0)
			{
				rig.velocity = new Vector3(0, rig.velocity.y, 0) + transform.forward.normalized * 5f;
			}
			else
			{
				rig.velocity = new Vector3(0, rig.velocity.y, 0);
			}
			isAirDashing = false;
		}

		// Check if we are against a wall, and if so, remove any forward momentum
		if (Physics.Raycast(transform.position, transform.forward, col.radius + 0.1f, layerMask))
		{
			rig.velocity = new Vector3(0, rig.velocity.y, 0);
		}
		
		groundCheckDistance = rig.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
	}

	void HandleDashMovement(bool dash)
	{
		// Apply a forward velocity according to dashing speed specified
		rig.velocity = transform.forward*dashingSpeed;

		// Reset Dash to allow for another input later
//		if (!anim.IsInTransition(0))
//		{
//			anim.SetBool("Dash",false);
//		}

		if (!dash)
		{
			anim.SetBool("Dash",false);
		}

	}

	void HandleVerticleDashMovement()
	{
		rig.velocity = transform.up*jumpPower;
	}

	void ScaleCapsuleForCrouching(bool crouch)
	{
		// Create layer mask for specified raycasts
		int layerMask = (1 << 9) + (1 << 10) + (1 << 11) + (1 << 2);
		layerMask = ~layerMask;

		// Alter sphere cast size to match player size
		float sphereRadiusScale = (normalSizedState == true) ? 1f : targetShrinkScale;

		// Check if crouching is allowed and scale capsule accordingly and when to leave the crouching state
		if (isGrounded && crouch)
		{
			if (isCrouching) return;
			col.height = col.height / 1.5f;
			col.center = col.center / 1.5f;
			isCrouching = true;
		}
		else if (isGrounded && !crouch && isCrouching)
		{
			Ray crouchRay = new Ray(rig.position * sphereRadiusScale + Vector3.up * col.radius * k_Half * sphereRadiusScale, Vector3.up);
			float crouchRayLength = capsuleHeight * sphereRadiusScale - col.radius * k_Half * sphereRadiusScale;
			if (Physics.SphereCast(crouchRay, col.radius * k_Half * sphereRadiusScale, crouchRayLength, layerMask))
			{
				isCrouching = true;
				return;
			}
			col.height = capsuleHeight;
			col.center = capsuleCenter;
			isCrouching = false;
		}
		else if (!isGrounded)
		{
			col.height = capsuleHeight;
			col.center = capsuleCenter;
			isCrouching = false;
		}
	}

	void PreventStandingInLowHeadroom()
	{
		// Create layer mask for specified raycasts
		int layerMask = (1 << 9) + (1 << 10) + (1 << 11) + (1 << 2);
		layerMask = ~layerMask;

		// Alter sphere cast size to match player size
		float sphereRadiusScale = (normalSizedState == true) ? 1f : targetShrinkScale;


		// Prevent standing up in crouch-only zones
		if (!isCrouching && anim.GetBool("Crouch"))
		{
			Ray crouchRay = new Ray(rig.position * sphereRadiusScale + Vector3.up * col.radius * k_Half * sphereRadiusScale, Vector3.up);
			float crouchRayLength = capsuleHeight * sphereRadiusScale - col.radius * k_Half * sphereRadiusScale;
			if (Physics.SphereCast(crouchRay, col.radius * k_Half * sphereRadiusScale, crouchRayLength, layerMask))
			{
				isCrouching = true;
			}
		}
	}


	void UpdateAnimator(Vector3 move)
	{
		// Grab current state to check the latest state of the animator for this function
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		// Update the animator parameters
		// If we are currently dashing, lockout any turning or movement application
		if (!currentBaseState.IsName("Dash"))
		{
			anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
			anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
		}

		// If we initiated an attack, reset Attack to allow for another action
		if (currentAttackState.IsName("Slash"))
		{
			anim.SetBool("Attack",false);
		}

		if (currentAttackState.IsName("Shoot"))
		{
			anim.SetBool("Shoot",false);
		}


		// Update state transition parameters
		anim.SetBool("Crouch", isCrouching);
		anim.SetBool("OnGround", isGrounded);
		anim.SetBool ("Guard", isGuarding);

		if (!isGrounded && !isAirDashing)
		{
			anim.SetFloat("Jump", rig.velocity.y);
		}

		// Determine which leg is lagging
		float runCycle =
			Mathf.Repeat(
				currentBaseState.normalizedTime + runCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * forwardAmount;

		// Tell animator which leg is lagging for use with transitions
		if (isGrounded)
		{
			anim.SetFloat("JumpLeg", jumpLeg);
		}

		// Alter animation speed while on ground
		if (isGrounded && move.magnitude > 0)
		{
			anim.speed = animSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			anim.speed = 1;
		}
	}

	public void OnAnimatorMove()
	{
		// Override default root motion
		if (isGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (anim.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

			// Preserve the existing y part of the current velocity.
			v.y = rig.velocity.y;
			rig.velocity = v;
		}
	}

	private void Shrink()
	{
		// Every update, change the scale of the gameobject by a time divisible amount
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one*targetShrinkScale, Time.deltaTime*sizeChangeSpeed);

		// Once target sized is roughly reached, cap the scale to the target scale and finalize size change transition parameters
		if (transform.localScale.x <= targetShrinkScale + 0.01f)
		{
			transform.localScale = Vector3.one*targetShrinkScale;
			sizeChangeTransition = false;
			normalSizedState = false;
			shrinkingActivated = false;

			// Change movement speed to match current size state
			dashingSpeed = shrunkDashingSpeed;
			jumpPower = shrunkJumpPower;
			moveSpeedMultiplier = shrunkMoveSpeedMultipler;
			animSpeedMultiplier = shrunkAnimSpeedMultiplier;
			rig.mass = shrunkMass;
		}
	}
	
	private void Grow()
	{
		// Every update, change the scale of the gameobject by a time divisible amount
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime*sizeChangeSpeed);

		// Every update, change the scale of the gameobject by a time divisible amount
		if (transform.localScale.x >= 1f - 0.01f)
		{
			transform.localScale = Vector3.one;
			sizeChangeTransition = false;
			normalSizedState = true;
			growingActivated = false;

			// Change movement speed to match current size state
			dashingSpeed = normalDashingSpeed;
			jumpPower = normalJumpPower;
			moveSpeedMultiplier = normalMoveSpeedMultiplier;
			animSpeedMultiplier = normalAnimSpeedMultiplier;
			rig.mass = normalMass;
		}
	}
}