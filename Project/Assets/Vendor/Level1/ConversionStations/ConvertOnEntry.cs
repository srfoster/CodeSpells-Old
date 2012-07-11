using UnityEngine;
using System.Collections;

public abstract class ConvertOnEntry : MonoBehaviour {
		
	void OnTriggerStay(Collider col)
	{
		// Check to see if a plant is colliding with us
		if(isInput(col.gameObject))
		{
			// Convert the plant to the target object
			convert(col.gameObject, target());
		}
	}
	
	void convert(GameObject first, GameObject second)
	{
		 GameObject crate = (Instantiate(second, first.transform.position, first.transform.rotation) as GameObject);
		
		crate.transform.position = 
			new Vector3(crate.transform.position.x, Terrain.activeTerrain.SampleHeight(crate.transform.position), crate.transform.position.z);
		
		Destroy(first);
	}
	
	abstract protected bool isInput(GameObject l);
	abstract protected GameObject target();
}
