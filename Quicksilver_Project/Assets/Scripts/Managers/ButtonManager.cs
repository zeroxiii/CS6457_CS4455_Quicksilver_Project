// Team Quicksilver
// Rahmaan Lodhia
// William Gu
// Larry He
// Mitch Leff

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonManager : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IUpdateSelectedHandler
{
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (EventSystem.current.currentSelectedGameObject != this.gameObject && anim.enabled && !anim.GetCurrentAnimatorStateInfo(0).IsName("Normal"))
			anim.SetTrigger("Normal");
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		EventSystem.current.SetSelectedGameObject(this.gameObject);
	}
	
	public void OnDeselect (BaseEventData eventData)
	{
		anim.SetTrigger("Normal");
	}	

	public void OnSelect (BaseEventData eventData)
	{
		if (anim != null)
		{
			anim.SetTrigger("Highlighted");
			anim.SetTrigger("SelectionTrigger");
		}
	}

	public void OnUpdateSelected (BaseEventData eventData)
	{
		if (anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Highlighted"))
		{
			anim.SetTrigger("Highlighted");
			anim.SetTrigger("SelectionTrigger");
		}
	}
}
