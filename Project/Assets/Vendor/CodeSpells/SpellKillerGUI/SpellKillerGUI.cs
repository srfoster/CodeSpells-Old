using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpellKillerGUI : MonoBehaviour {
	
	List<GameObject> enchanted_objects = new List<GameObject>();
	
	private Texture2D[] frames;
	private GUIStyle label_style;

	void Start () {
		frames = new Texture2D[12];
		for(int i = 0; i < 12; i++)
		{
			frames[i] = Resources.Load("Unloader/loader_frame_"+(i+1)) as Texture2D;
		}
				
		label_style = new GUIStyle();
		label_style.fontSize = 20;
		label_style.normal.textColor = Color.white;

		Enchantable.EnchantmentStarted += (target, class_name) => {
			enchanted_objects.Add(target);
		};
		
		Enchantable.EnchantmentEnded += (target, class_name) => {
			enchanted_objects.Remove(target);
		};
	}
	
	
	void OnGUI(){
		List<GameObject> to_remove = new List<GameObject>();
		
		foreach(GameObject obj in enchanted_objects)
		{
			if(!obj.GetComponent<Enchantable>().isEnchanted())
				to_remove.Add(obj);
		}
		
		foreach(GameObject obj in to_remove)
			enchanted_objects.Remove(obj);
		
		
		
		int index = Mathf.FloorToInt(Time.time * 20) % frames.Length;
		Texture2D texture = frames[index];	
		
		GUI.BeginGroup(new Rect(10, 30, 200, Screen.height / 2));
		int count = 0;
		foreach(GameObject obj in enchanted_objects)
		{
			Enchantable e = obj.GetComponent<Enchantable>();
			string name = e.getFileName();
			
			
			if(GUI.Button(new Rect(0,count * 55, 200, 40), ""))
			{
				obj.GetComponent<Enchantable>().disenchant();	
			}
			
			GUI.DrawTexture(new Rect(10,count * 55 + 7,25,25),texture);
			GUI.Label(new Rect(55, count * 55 + 10, 100, 25), name);
		
			count++;
		}
		GUI.EndGroup();
	}
}
