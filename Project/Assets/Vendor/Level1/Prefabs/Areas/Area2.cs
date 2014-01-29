using UnityEngine;
using System.Collections;

public class Area2 : MonoBehaviour {
	public bool area2 = false;

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("*************************" + area2);
		//if not the player colliding
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		if(area2)
		{
			Popup.mainPopup.popup("You've entered The Rainy Grove!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for the Rainy Grove yet.");
		}
	}
}
