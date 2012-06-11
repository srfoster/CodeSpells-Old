using UnityEngine;
using System.Collections;

public class PlantToCrate : ConvertOnEntry {
	
	public GameObject output;

	protected override bool isInput(GameObject plant)
	{
		if(plant.GetComponent("Growable") == null)
			return false;
		
		if(collider.gameObject.GetComponent<Substance>() != null && collider.gameObject.GetComponent<Substance>().isPlant())
			return false;
		
		return (plant.GetComponent("Growable") as Waterable).isWaterlogged();
	}
	
	protected override GameObject target()
	{
		return output;	
	}
}