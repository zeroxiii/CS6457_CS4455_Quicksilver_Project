#pragma strict


function OnTriggerEnter (obj : Collider) {
	var thedoor = gameObject.FindWithTag("New Game");
	thedoor.GetComponent.<Animation>().Play("open");
	yield WaitForSeconds(2);
	Application.LoadLevel("quicksilver_m4");
}