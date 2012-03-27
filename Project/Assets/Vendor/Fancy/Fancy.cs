using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System;



public class Fancy : MonoBehaviour {
	static Hashtable objects = new Hashtable();
	public static string current_file_loc = "file";
	public static GameObject current_object = null;
	public static MonoBehaviour current_behaviour = null;
	public static int current_line_number = 0;
	static GameObject temp;

	public static GameObject InstantiatePrefab(GameObject prefab, Vector3 location, Quaternion rotation)
	{
        StackTrace st = new StackTrace(true);
		
		GameObject obj = Instantiate(prefab,location,rotation) as GameObject;
		
		objects.Add(obj, st.GetFrame(1));
		
		obj.AddComponent<FancyGizmos>();
		
		return obj;
	}
	
	public static void Clear(){
		objects = new Hashtable();	
	}
	
	public static void gizmoFor(GameObject obj) {
		Gizmos.DrawIcon(obj.transform.position, "smaller_scroll.png");
    }
	
	public static void setSelected(GameObject obj){

		if(objects[obj] == null)
			return;
		
		if(current_object == obj)
			return;
		
		current_file_loc = objects[obj].ToString();
		current_object = obj;
		
		string[] split_loc = current_file_loc.Split('/');
		string short_loc = split_loc[split_loc.Length - 1];
		
		string[] split_short_loc = short_loc.Split(':');
		
		string short_file_name = split_short_loc[0];
		string line_number = split_short_loc[1];
		
		current_line_number = Int32.Parse(line_number);
		
		string type_name = short_file_name.Split('.')[0];
		
		if(temp == null)
			temp = new GameObject("temp");

		
		temp.AddComponent(type_name);
		
		MonoBehaviour mb = temp.GetComponent(type_name) as MonoBehaviour;
		
		current_behaviour = mb;
		
		
		UnityEngine.Debug.Log(Fancy.current_behaviour);
		
	}
}