using UnityEngine;
using System.Collections;

public class Area1 : MonoBehaviour {
	public bool area1 = true;
	
	void OnTriggerEnter(Collider col)
	{
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area1)
		{
			Popup.mainPopup.popup("You've entered area 1!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for area 1 yet.");
		}
	}
}
