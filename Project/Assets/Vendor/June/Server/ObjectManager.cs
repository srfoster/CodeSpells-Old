using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ObjectManager {
	
	static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
	
	public static void Register(GameObject obj)
	{
//		Debug.Log("Enchantable: " + obj.name + " " + obj.GetInstanceID().ToString());
		
		try{
			objects.Add(obj.GetInstanceID().ToString(), obj);
		}catch(Exception e){
			Debug.Log("Could not register. " + e);	
		}
	}
	
	public static void Register(GameObject obj, string id)
	{
	
		//Debug.Log ("Trying to register " + id);
		objects.Add(id, obj);
	}
	
	public static void Unregister(string id)
	{
		//Debug.Log ("Unregistering " + id);
		objects.Remove(id);	
	}
	
	public static void Reregister(GameObject ob, string id, string new_id)
	{
		if(objects.ContainsKey(new_id))
		{
			throw new System.ArgumentException();
		}
		
		if(!objects.ContainsKey(id))
		{
			Register(ob,new_id);
			
			return;
		}
		
		GameObject obj = objects[id];
		objects.Remove(id);
		objects.Add(new_id,obj);
	}

	public static GameObject FindById(string id)
	{
		
		return objects[id];	
	}
	
	public static string GetID(GameObject g)
	{
		foreach(KeyValuePair<string,GameObject> entry in objects) {
			if (entry.Value.GetHashCode().Equals(g.GetHashCode())) {
				return entry.Key;
				break;
			}
		}
		return "UnregisteredObject";
	}
	
	public static Dictionary<string, GameObject> GetObjects()
	{
		return objects;	
	}
}
