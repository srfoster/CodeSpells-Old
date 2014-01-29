using UnityEngine;
using System.Collections;

public class Area3 : MonoBehaviour {
	public bool area3 = false;

	void OnTriggerEnter(Collider col)
	{
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area3)
		{
			Popup.mainPopup.popup("You've entered The Swamp!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Swamp yet.");
		}
	}
}