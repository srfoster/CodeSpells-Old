using UnityEngine;
using System.Collections;

public class Area5 : MonoBehaviour {
	public bool area5 = false;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area5)
		{
			Popup.mainPopup.popup("You've entered The Village Square!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Village Square yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
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