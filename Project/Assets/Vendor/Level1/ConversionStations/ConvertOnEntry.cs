using UnityEngine;
using System.Collections;

public abstract class ConvertOnEntry : MonoBehaviour {
		
	void OnTriggerEnter(Collider col)
	{
		if(isInput(col.gameObject))
		{
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
