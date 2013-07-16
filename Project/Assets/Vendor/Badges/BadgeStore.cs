using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BadgeStore {
	
	Dictionary<string, BadgeInfo> badges = new Dictionary<string, BadgeInfo>();
	List<string> names = new List<string>();
	
	public void Add(string name, string label, string path){
		if(!badges.ContainsKey(name))
		{
			badges.Add(name, new BadgeInfo(label, path));
			names.Add(name);
		}
	}
	
	public void Replace(string name, string new_name, string label, string path){
		if(badges.ContainsKey(name))
		{
			badges.Add(new_name, new BadgeInfo(label, path));

			badges.Remove(name);
				
			int i = names.IndexOf(name);
			names[i] = new_name;
		}
	}
	

	
	public string label(int i)
	{
		return badges[names[i]].label;
	}
	
	public Texture2D icon(int i)
	{
		return badges[names[i]].texture;	
	}
	
	public string path(int i)
	{
		return badges[names[i]].path;	
	}
	
	public string name(int i)
	{
	    return names[i];
	}
	
	public BadgeInfo Get(int i)
	{
		return badges[names[i]];	
	}
	
	public string label(string i)
	{
		return badges[i].label;
	}
	
	public Texture2D icon(string i)
	{
		return badges[i].texture;	
	}
	
	public string path(string i)
	{
		return badges[i].path;	
	}
	
	public BadgeInfo Get(string i)
	{
		return badges[i];	
	}
	
	public bool Contains(string name)
	{
		return names.Contains(name);	
	}
	
	public int Size()
	{
		return names.Count;	
	}
	
	public class BadgeInfo{
		public Texture2D texture;	
		public string label;
		public string path;
		public bool buttonUnlockable;
		
		public BadgeInfo(string label, string path)
		{
			texture = Resources.Load(path) as Texture2D;
			this.label = label;
			this.path = path;
			buttonUnlockable = false;
		}
		
		public void MakeButtonUnlockable() {
		    buttonUnlockable = true;
		}
	}
}
