using UnityEngine;
using System.Collections;

public class RockToCrate : ConvertOnEntry {
	
	public GameObject output;

	protected override bool isInput(GameObject l)
	{
		if(l.GetComponent("Flamable") == null)
			return false;
		
		if(!(l.GetComponent("Flamable") as Flamable).isIgnited())
			return false;
		
		if(!(l.tag.Equals("Rock")))
			return false;
		
		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}
