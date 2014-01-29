using UnityEngine;
using System.Collections;

public class Area5 : MonoBehaviour {
	public bool area5 = false;

	void OnTriggerEnter(Collider col)
	{
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area5)
		{
			Popup.mainPopup.popup("You've entered The Village Square!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Village Square yet.");
		}
	}
}