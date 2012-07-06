using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;

public class Util {

	public GameObject instantiate(GameObject obj, Vector3 loc, Quaternion rot)
	{
		return UnityEngine.Object.Instantiate (obj, loc, rot) as GameObject;	
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
