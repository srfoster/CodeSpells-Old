using UnityEngine;
using System.Collections;

public class Area6 : MonoBehaviour {
	public bool area6 = false;

	void OnTriggerEnter(Collider col)
	{
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area6)
		{
			Popup.mainPopup.popup("You've entered The Firepits!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Firepits yet.");
		}
	}
}