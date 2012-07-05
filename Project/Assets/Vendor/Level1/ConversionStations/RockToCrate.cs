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
		if(l.GetComponent("Substance") == null)
			return false;
		if(!(l.GetComponent("Substance") as Substance).isRock())
			return false;
		return true;
	}
	
	protected override GameObject target()
	{
		Debug.Log("Converting to: "+output.gameObject.name);
		return output;	
	}
}
