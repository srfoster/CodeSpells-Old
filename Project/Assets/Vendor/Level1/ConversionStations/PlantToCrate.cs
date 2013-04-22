using UnityEngine;
using System.Collections;

public class PlantToCrate : ConvertOnEntry {
	
	public GameObject output;

	protected override bool isInput(GameObject plant)
	{

		if(plant.GetComponent("Substance") == null)
		{
			Debug.Log("Was not a substance");
			return false;
		}
		
		if(!(plant.GetComponent("Substance") as Substance).isPlant())
		{
			Debug.Log("Was not a plant");
			return false;
		}
		
		if(plant.GetComponent("Growable") == null)
		{
			Debug.Log("Was not growable");
			return false;
		}

		if(!(plant.GetComponent("Growable") as Waterable).isWaterlogged())
		{
			Debug.Log("Was not waterlogged");
			return false;
		}
		
		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}