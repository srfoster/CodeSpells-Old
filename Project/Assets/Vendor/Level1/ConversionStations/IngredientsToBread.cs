using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientsToBread : MonoBehaviour {

	List<GameObject> numPlants = new List<GameObject>();
	List<GameObject> numRocks = new List<GameObject>();
	
	//takes in a generic bread object which it may or may not create
	public GameObject breadRegion;
		
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.GetComponent<Ingredient>() == null)
			return;
		if(col.gameObject.GetComponent<Ingredient>().isRock()) {
<<<<<<< HEAD
			//Debug.Log("Found a rock ingredient!");
			numRocks.Add (col.gameObject);
		}
		else if(col.gameObject.GetComponent<Ingredient>().isPlant()) {
			//Debug.Log("Found a plant ingredient!");
=======
			numRocks.Add (col.gameObject);
		}
		else if(col.gameObject.GetComponent<Ingredient>().isPlant()) {
>>>>>>> 1b6883993580a7b3d72ff2754962f1d7e11dafef
			numPlants.Add (col.gameObject);
		}
		else
			return;
		
		if((numRocks.Count > 0) && (numPlants.Count > 0)) {
<<<<<<< HEAD
			//Debug.Log("I can make bread!");
=======
>>>>>>> 1b6883993580a7b3d72ff2754962f1d7e11dafef
			//create a bread object
			Destroy(numRocks[0]);
			numRocks.RemoveAt (0);
			Destroy(numPlants[0]);
			numPlants.RemoveAt (0);
			
			breadRegion.GetComponent<BreadStation>().addBread();
			
			return;
		}
	}
}