using UnityEngine;
using System.Collections;

public class Area4 : MonoBehaviour {
	public bool area4 = false;
	
	void OnCollisionEnter(Collision col)
	{
		
		
		if(area4)
		{
			Popup.mainPopup.popup("You've entered The Monster's Hideaway!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Monster's Hideawy yet.");
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if(area4)
		{
			Popup.mainPopup.popup("You've entered The Monster's Hideaway!");	
		}
		else
		{
			Popup.mainPopup.popup("You don't have enough magic for The Monster's Hideaway yet.");
		}
	}
}