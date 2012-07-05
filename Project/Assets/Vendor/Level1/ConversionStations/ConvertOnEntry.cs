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
		Instantiate(second, first.transform.position, first.transform.rotation);
		
		Destroy(first);
	}
	
	abstract protected bool isInput(GameObject l);
	abstract protected GameObject target();
}
