using UnityEngine;
using System.Collections;

public class PlantToCrate : ConvertOnEntry {
	
	public GameObject output;

	protected override bool isInput(GameObject plant)
	{

		if(plant.GetComponent("Substance") == null)
			return false;
		
		if(!(plant.GetComponent("Substance") as Substance).isPlant())
			return false;
		if(plant.GetComponent("Growable") == null)
			return false;

		if(!(plant.GetComponent("Growable") as Waterable).isWaterlogged())
			return false;

		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}