#pragma strict


function OnTriggerEnter (obj : Collider) {
	var thedoor = gameObject.FindWithTag("Exit");
	thedoor.GetComponent.<Animation>().Play("open");
	yield WaitForSeconds(2);
	Application.Quit();
}