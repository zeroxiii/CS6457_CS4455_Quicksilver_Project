#pragma strict


function OnTriggerEnter (obj : Collider) {
	var thedoor = gameObject.FindWithTag("Level Select");
	thedoor.GetComponent.<Animation>().Play("open");
	yield WaitForSeconds(2);
	//Application.LoadLevel("quicksilver_m4");
}