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
		
		output.AddComponent<Ingredient>();
		output.GetComponent<Ingredient>().setIsRock();
		
		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}
