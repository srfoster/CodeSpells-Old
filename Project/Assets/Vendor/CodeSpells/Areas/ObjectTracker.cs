using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectTracker : MonoBehaviour {
	
	private List<GameObject> within = new List<GameObject>();

	void OnTriggerEnter(Collider col)
	{	
		//Debug.Log ("Tracker adding");
		within.Add(col.gameObject);
	}
	
	void OnTriggerExit(Collider col)
	{
		within.Remove(col.gameObject);
	}
	
	public List<GameObject> getWithin()
	{
		return within;	
	}
}
