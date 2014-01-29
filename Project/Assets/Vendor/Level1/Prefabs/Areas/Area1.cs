using UnityEngine;
using System.Collections;

public class Area1 : MonoBehaviour {
	public bool area1 = true;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area1)
		{
			Popup.mainPopup.popup("You've entered area 1!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for this area yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
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
