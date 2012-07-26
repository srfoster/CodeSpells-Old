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
				
		if(parent.GetComponent<ObjectTracker>() == null)
			return "";
		
		List<string> ids = new List<string>();
		
		parent.GetComponent<ObjectTracker>().getWithin();
		
		foreach(GameObject child in parent.GetComponent<ObjectTracker>().getWithin())
		{
			
			if(child != null && child.GetComponent<Enchantable>() != null)
			{
				ids.Add(child.GetComponent<Enchantable>().getId());
			}
		}
		
		string[] id_array = ids.ToArray();
		
		return string.Join(";",id_array);		
	}
	
	public string getEnchantedChildrenOf(string id)
	{
		GameObject parent = ObjectManager.FindById(id);
		
		List<string> ids = new List<string>();
		
		foreach(Transform child in parent.transform)
		{
			if(child.gameObject.GetComponent<Enchantable>() != null)
			{
				ids.Add(child.GetComponent<Enchantable>().getId());
			}
		}
		
		string[] id_array = ids.ToArray();
		
		return string.Join(";",id_array);
	}
	
	public static bool isOfType(string id, int type) {
		GameObject g = ObjectManager.FindById(id);
		
		switch(type) {
		case 1://rock
			return ((g.name).StartsWith("rock_with_collider"));
		case 2://plant
			return ((g.name).StartsWith("plant"));
		case 3://seed
			return ((g.name).Contains("Seed"));
		case 4://rocksugar
			return ((g.name).StartsWith("RockSugar"));
		case 5://flour
			return ((g.name).StartsWith("Flour"));
		case 6://bread
			return ((g.name).Contains("bread"));
		case 7://hasIgnited
			return g.GetComponent<Flamable>().isIgnited();
		default:
			return false;
		}
	}
	
	public static string getObjWith (string idCenter, string idName, double radius) {
		int counter = 0;
		GameObject center = ObjectManager.FindById(idCenter);
		GameObject named = ObjectManager.FindById(idName);
		string ids = "";
		
		bool isFirst = true;
		
		if (named.GetComponent("Enchantable")) {
			Collider[] nearColliders = Physics.OverlapSphere(center.transform.position, (float)radius);
			foreach (Collider col in nearColliders) {
				if ((col.gameObject.GetComponent("Enchantable") != null) && ((named.name).Equals (col.transform.name))) {
					
					counter++;
					ids += (isFirst) ? "" : ";";
					isFirst = false;
					ids += ((col.gameObject.GetComponent("Enchantable") as Enchantable).getId());
				}
			}
		}
		return ids;
	}
}
