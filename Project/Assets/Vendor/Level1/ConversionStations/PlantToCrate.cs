using UnityEngine;
using System.Collections;

public class PlantToCrate : ConvertOnEntry {
	
	public GameObject output;

	protected override bool isInput(GameObject l)
	{
		if(l.GetComponent("Waterable") == null)
			return false;
		
		if(!(l.GetComponent("Flamable") as Waterable).isWaterlogged())
			return false;
		
		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}