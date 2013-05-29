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
	
	public String spawnOverNetwork(String prefabName, Vector3 pos) {
		
		GameObject.Find("CrateGUI").GetComponent<CrateGUI>().IncrementCrateCount();
		
		GameObject temp =  (GameObject) Network.Instantiate(Resources.Load(prefabName) as GameObject, pos, Quaternion.identity, 0);
		ObjectManager.Register(temp, ""+temp.GetHashCode());
		return ""+temp.GetHashCode();
	}
	
	public string size(string id)
	{
		GameObject parent = ObjectManager.FindById(id);
		string ret = parent.collider.bounds.size.x + "," + parent.collider.bounds.size.y + "," + parent.collider.bounds.size.z;
		
		return ret;
	}
	
	public void popup(string s)
	{
		Popup.mainPopup.popup(s);	
	}
	

	public void reregister(string old_id, string new_id)
	{
		ObjectManager.Reregister(ObjectManager.FindById(old_id), old_id, new_id);
	}
	
	public string getWithin(string id)
	{
		numTimesCalled++;
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
		foreach (string i in id_array) {
		    logObj("area", i, id);
		}
		
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
		    if (g.GetComponent<Flamable>())
			    return g.GetComponent<Flamable>().isIgnited();
			return false;
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
	
	public static string logObj(string when, string id, string spellname) {
	       GameObject g = ObjectManager.FindById(id);
	       
	       // Object id, type/name, position, ground height at position, is on fire
	       bool flaming = false;
	       if (g.GetComponent<Flamable>())
	            flaming = g.GetComponent<Flamable>().isIgnited();
	       TraceLogger.LogKV("object"+when, spellname+", "+id+", "+g.name+", "+g.transform.position+", "+Terrain.activeTerrain.SampleHeight(g.transform.position)+", "+flaming);
	       Util util = new Util();
	       if (when.Equals("end") && g.name.Equals("Flag(Clone)")) {
	            util.getWithin(id);
// 	            foreach (string i in util.getWithin(id).Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries)) {
// 	                GameObject gobj = ObjectManager.FindById(i);
//            
//                     // Object id, type/name, position, ground height at position, is on fire
//                     flaming = false;
//                     if (gobj.GetComponent<Flamable>())
//                         flaming = gobj.GetComponent<Flamable>().isIgnited();
//                     TraceLogger.LogKV("object"+when, spellname+", "+i+", "+gobj.name+", "+gobj.transform.position+", "+Terrain.activeTerrain.SampleHeight(gobj.transform.position)+", "+flaming);
// 	            }
	       }

	       return g.name;
	}
	
	public static bool isFlammable(string id) {
	    GameObject g = ObjectManager.FindById(id);
	    if (g.GetComponent<Flamable>()) {
	        TraceLogger.LogKV("flammable", id+", "+g.name+", True");
	        return true;
	    }
	    TraceLogger.LogKV("flammable", id+", "+g.name+", False");
	    return false;
	}
	
// 	public void endCast(string spellname) {
// 	    //spellname, time, position of objects and player
// 	    TraceLogger.LogKV("endspell", spellname+", "+Time.time);
// 	}

}
