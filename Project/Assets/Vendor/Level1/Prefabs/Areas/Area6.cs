using UnityEngine;
using System.Collections;

public class Area6 : MonoBehaviour {
	public bool area6 = false;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area6)
		{
			Popup.mainPopup.popup("You've entered The Firepits!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Firepits yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
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