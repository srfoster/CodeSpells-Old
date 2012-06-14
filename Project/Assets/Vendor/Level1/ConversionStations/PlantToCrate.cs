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
		
		
		output.AddComponent<Ingredient>();
		output.GetComponent<Ingredient>().setIsPlant();
		
		//if(collider.gameObject.GetComponent<Substance>() != null && collider.gameObject.GetComponent<Substance>().isPlant())
		//	return false;
		
		//Debug.Log("The plant: "+plant.gameObject+" is waterlogged: "+ (plant.GetComponent("Growable") as Waterable).isWaterlogged());
		
		//return (plant.GetComponent("Growable") as Waterable).isWaterlogged();
		return true;
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}