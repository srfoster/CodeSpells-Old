using UnityEngine;
using System.Collections;

public class Area4 : MonoBehaviour {
	public bool area4 = false;

	void OnTriggerEnter(Collider col)
	{
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area4)
		{
			Popup.mainPopup.popup("You've entered The Monster's Hideaway!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Monster's Hideaway yet.");
		}
	}
}