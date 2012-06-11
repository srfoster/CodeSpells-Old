using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientsToBread : MonoBehaviour {

	List<GameObject> numPlants = new List<GameObject>();
	List<GameObject> numRocks = new List<GameObject>();
	
	//takes in a generic bread object which it may or may not create
	public GameObject bread;
	public GameObject breadRegion;
		
			
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
			Vector3 breadPos = new Vector3(breadRegion.transform.position.x, 0, breadRegion.transform.position.z);
			Instantiate(bread, breadPos, breadRegion.transform.rotation);
			
			return;
		}
		
	}
}