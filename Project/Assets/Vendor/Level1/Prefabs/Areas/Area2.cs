using UnityEngine;
using System.Collections;

public class Area2 : MonoBehaviour {
	public bool area2 = false;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area2)
		{
			Popup.mainPopup.popup("You've entered The Rainy Grove!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Rainy Grove yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
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
