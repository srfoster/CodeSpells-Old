using UnityEngine;
using System.Collections;

public class Attractive : MonoBehaviour {

	void onTriggerEnter(GameObject collider)
	{
		// If the other person is attracted to you too, move to them
		if(collider.gameObject.GetComponent<Attractive>() != null)
		{
			moveTo(collider.gameObject);
		}
	}
	
	void moveTo(GameObject collector)
	{
		transform.LookAt(collector.transform);
		
	}
}