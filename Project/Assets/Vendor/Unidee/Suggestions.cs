using UnityEngine;
using System.Collections;

public class Suggestions {
	
	ArrayList strings = new ArrayList();
	string max = "";
	
	public void put(string s)
	{
		if(s.Length > max.Length)
			max = s;
		
		strings.Add(s);
	}

	public int number()
	{
		return strings.Count;
	}
	
	public string longest()
	{
		return max;
	}
	
	public string toString()
	{
		strings.Sort();
		
		string ret = "";
		foreach(string s in strings)
		{
			ret += s + "\n";
		}
		
		return ret.Substring(0,ret.Length-1);
	}
}
