// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff


using RAIN.Action;
using RAIN.Core;
using UnityEngine;

[RAINAction]
public class DetermineTargetLocation : RAINAction
{
	EnemyShooting enemyShoot;
	GameObject playerTarget;

	public override void Start (AI ai)
	{
		enemyShoot = ai.Body.GetComponent<EnemyShooting>();
		playerTarget = ai.WorkingMemory.GetItem<GameObject>("PlayerTargetDetected");
	}


	public override ActionResult Execute(AI ai)
	{
		enemyShoot.targetPosition = playerTarget.transform.position + playerTarget.GetComponent<Rigidbody>().velocity*0.2f;
		return ActionResult.SUCCESS;
	}
}