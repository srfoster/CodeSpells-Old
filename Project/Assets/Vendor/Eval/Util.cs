using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;

public class Util {
	public int numTimesCalled = 0;

	public GameObject instantiate(GameObject obj, Vector3 loc, Quaternion rot)
	{
		return UnityEngine.Object.Instantiate (obj, loc, rot) as GameObject;	
	}
	
	public void reregister(string old_id, string new_id)
	{
		ObjectManager.Reregister(ObjectManager.FindById(old_id), old_id, new_id);
	}
	
	public string getWithin(string id)
	{
		numTimesCalled++;
		Debug.Log ("getWithinWasCalled: #"+numTimesCalled);
		GameObject parent = ObjectManager.FindById(id);
		//Debug.Log ("parent is null? "+ (parent == null));
		//Debug.Log ("parent has been found");
				
		if(parent.GetComponent<ObjectTracker>() == null)
			return "";
		
		List<string> ids = new List<string>();
		
		parent.GetComponent<ObjectTracker>().getWithin();
		//Debug.Log ("getWithin() was called on the parent's component");
		
		foreach(GameObject child in parent.GetComponent<ObjectTracker>().getWithin())
		{
			//Debug.Log ("Found a child GameObject: "+child);
			
			if(child != null && child.GetComponent<Enchantable>() != null)
			{
				ids.Add(child.GetComponent<Enchantable>().getId());
				//Debug.Log ("enchantable object has been found");
			}
		}
		//Debug.Log ("Has finished looping through enchantable objects");
		
		string[] id_array = ids.ToArray();
		
		return string.Join(";",id_array);		
	}
	
	public string getEnchantedChildrenOf(string id)
	{
		GameObject parent = ObjectManager.FindById(id);
		
		//Debug.Log("getting enchanted children.  parent = " + parent.name);
		
		List<string> ids = new List<string>();
		
		foreach(Transform child in parent.transform)
		{
			//Debug.Log("Looking at child " + child.gameObject.name);
			if(child.gameObject.GetComponent<Enchantable>() != null)
			{
				//Debug.Log("Found enchanted child = " + child.gameObject.name);

				ids.Add(child.GetComponent<Enchantable>().getId());
			}
		}
		
		string[] id_array = ids.ToArray();
		
		return string.Join(";",id_array);
	}
	
	public static string getObjWith (string idCenter, string idName, double radius) {
		int counter = 0;
		GameObject center = ObjectManager.FindById(idCenter);
		GameObject named = ObjectManager.FindById(idName);
		string ids = "";
		
		bool isFirst = true;
		
		if (named.GetComponent("Enchantable")) {
			Collider[] nearColliders = Physics.OverlapSphere(center.transform.position, (float)radius);
			//Debug.Log ("nearColliders.Length() is "+nearColliders.);
			foreach (Collider col in nearColliders) {
				if ((col.gameObject.GetComponent("Enchantable") != null) && ((named.name).Equals (col.transform.name))) {
					
					//reset the ID to Found + i
					counter++;
					
					//(col.gameObject.GetComponent("Enchantable") as Enchantable).setId ("Found "+counter);
					ids += (isFirst) ? "" : ";";
					isFirst = false;
					ids += ((col.gameObject.GetComponent("Enchantable") as Enchantable).getId());
				}
			}
		}
		return ids;
	}
}
