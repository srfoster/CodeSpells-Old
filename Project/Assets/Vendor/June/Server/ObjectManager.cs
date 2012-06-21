using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectManager {
	
	static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
	
	public static void Register(GameObject obj)
	{
		Debug.Log("Enchantable: " + obj.name + " " + obj.GetInstanceID().ToString());
		
		objects.Add(obj.GetInstanceID().ToString(), obj);
		
	}

	public static GameObject FindById(string id)
	{
		
		return objects[id];	
	}
	
	public static Dictionary<string, GameObject> GetObjects()
	{
		return objects;	
	}
}
