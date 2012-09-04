using UnityEngine;
using System.Collections;

public class CrateCancelOut : MonoBehaviour {
	public string to_kill;
	
	void OnTriggerEnter(Collider col) {
		
		if(col.gameObject.name.StartsWith(to_kill))
		{
			//Destroy(col.gameObject);	
			Destroy(gameObject);
		}
		
		
	}
	
	void OnCollisionEnter(Collision col) {
		
		if(col.gameObject.name.StartsWith(to_kill))
		{
			//Destroy(col.gameObject);	
			Destroy(gameObject);
		}
	}
	
}
