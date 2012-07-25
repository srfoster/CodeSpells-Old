using UnityEngine;
using System.Collections;

public class Dome : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		
		Enchantable e = col.gameObject.GetComponent<Enchantable>();
		
		if(e != null)
		{
			e.disenchant();	
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		Enchantable e = col.gameObject.GetComponent<Enchantable>();
		
		if(e != null)
		{
			e.disenchant();	
		}
	}
}
