using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;

public class Util : MonoBehaviour {

	public GameObject instantiate(GameObject obj, Vector3 loc, Quaternion rot)
	{
		return Instantiate (obj, loc, rot) as GameObject;	
	}
	
	public static string getObjWith (string id, double radius) {
		int counter = 0;
		GameObject g = ObjectManager.FindById(id);
		string ids = "";
		
		bool isFirst = true;
		
		if (g.GetComponent("Enchantable")) {
			Collider[] nearColliders = Physics.OverlapSphere(g.transform.position, (float)radius);
			foreach (Collider col in nearColliders) {
				if ((col.gameObject.GetComponent("Enchantable") != null) && (col.gameObject.name.Equals (g.name))) {
					counter++;
					Debug.Log ("		FOUND ENCHANTED OBJECT #"+counter);
					ids += (isFirst) ? "" : ";";
					isFirst = false;
					ids += ((col.gameObject.GetComponent("Enchantable") as Enchantable).getId());
				}
			}
		}
		//Debug.Log ("returned id string is:"+ids);
		return ids;
	}
}
