using UnityEngine;
using System.Collections;

public class Area7 : MonoBehaviour {
	public bool area7 = false;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area7)
		{
			Popup.mainPopup.popup("You've entered The Ponds!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Ponds yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if(area7)
		{
			Popup.mainPopup.popup("You've entered The Ponds!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Ponds yet.");
		}
	}
}