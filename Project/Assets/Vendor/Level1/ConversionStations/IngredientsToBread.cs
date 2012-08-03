using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientsToBread : MonoBehaviour {

	List<GameObject> numPlants = new List<GameObject>();
	List<GameObject> numRocks = new List<GameObject>();
	
	//takes in a generic bread object which it may or may not create
	public GameObject breadRegion;
		
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.transform.parent != null)
			return;
		if(col.gameObject.GetComponent<Ingredient>() == null)
			return;

		if(col.gameObject.GetComponent<Ingredient>().isRock() && !numRocks.Contains(col.gameObject)) {
			numRocks.Add (col.gameObject);
		}
		else if(col.gameObject.GetComponent<Ingredient>().isPlant()&& !numPlants.Contains(col.gameObject)) {
			numPlants.Add (col.gameObject);
		}
		else
			return;

		if((numRocks.Count > 0) && (numPlants.Count > 0)) {
			//Debug.Log("I can make bread!");
			//create a bread object
			DestroyObjects();
			Destroy(numRocks[0]);
			numRocks.RemoveAt (0);
			Destroy(numPlants[0]);
			numPlants.RemoveAt (0);
			
			breadRegion.GetComponent<BreadStation>().addBread();
			
			return;
		}
	}
	
	public void DestroyObjects()
	{
		
	}
}
