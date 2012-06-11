using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientsToBread : MonoBehaviour {

	List<GameObject> numPlants = new List<GameObject>();
	List<GameObject> numRocks = new List<GameObject>();
	
	//Store one ingredient here
		
			
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.GetComponent<Ingredient>() == null)
			return;
		if(col.gameObject.GetComponent<Ingredient>().isRock()) {
			numRocks.Add (col.gameObject);
		}
		else if(col.gameObject.GetComponent<Ingredient>().isPlant()) {
			numPlants.Add (col.gameObject);
		}
		else
			return;
		
		if((numRocks.Count > 0) && (numPlants.Count > 0)) {
			//create a bread object
			
			Destroy(numRocks[0]);
			numRocks.RemoveAt (0);
			Destroy(numPlants[0]);
			numPlants.RemoveAt (0);
			return;
		}
		
	}
}