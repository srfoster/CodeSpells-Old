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

	public static string getType(string id) {
	       GameObject g = ObjectManager.FindById(id);
	       
	       TraceLogger.LogKV("object", id+", "+g.name+", "+g.transform.position+", "+Terrain.activeTerrain.SampleHeight(g.transform.position));

	       return g.name;
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
	
	public void log(string msg) {
	    // Function for sending messages to Unity to be added to the log and requesting Unity to add current game state to the log
	    
	    // player's position
	    GameObject me = ObjectManager.FindById("Me");
	    TraceLogger.LogKV("player", ""+me.transform.position);
	    
	    // inventory
	    Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
	    //string inventoryContents = inventory.listInventory();
	    List <GameObject> inventoryContents = inventory.getMatching("");
	    string s = "";
	    foreach (GameObject item in inventoryContents) {
	        if (!item.name.StartsWith("Book") && !item.name.StartsWith("Badges") && !item.name.StartsWith("InitialScroll")) {
	            s += item.name +", "+ item.GetInstanceID() + "; ";
	        }
	    }
	    char[] trim = {';',' '};
	    s = s.TrimEnd(trim);
	    TraceLogger.LogKV("inventory", s);
	    
	    return;
	}
	
	public static string logObj(string id) {
	       GameObject g = ObjectManager.FindById(id);
	       
	       TraceLogger.LogKV("object", id+", "+g.name+", "+g.transform.position+", "+Terrain.activeTerrain.SampleHeight(g.transform.position));

	       return g.name;
	}
	
	public void endCast(string spellname) {
	    //spellname, time, position of objects and player
	    TraceLogger.LogKV("endspell", spellname+", "+Time.time);
	}

}
