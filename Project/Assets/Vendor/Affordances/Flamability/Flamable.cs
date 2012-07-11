using UnityEngine;
using System.Collections;

public class Flamable : MonoBehaviour {
	public GameObject flames_prefab;
	public bool flaming_at_start = false;
	
	private GameObject flames_actual;
	
	void Start()
	{
		if(flaming_at_start)
		{
			Ignite();	
		}
	}

	public void Ignite()
	{
		if(isIgnited())
			return;
		
		flames_actual = Instantiate(flames_prefab, transform.position, Quaternion.identity) as GameObject;
		flames_actual.transform.parent = transform;
	}
	
	public bool isIgnited()
	{
		return flames_actual != null;
	}
	
	public void Extinguish()
	{
		if(!isIgnited())
			return;

		Destroy(flames_actual);
	}
	
	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.GetComponent<Substance>() != null && col.gameObject.GetComponent<Substance>().isWater())
		{
			Extinguish();
		}
		if(col.gameObject.GetComponent("Flamable") == null)
		{
			return;	
		}
		
		Flamable other_flamable = col.gameObject.GetComponent("Flamable") as Flamable;
		
		if(other_flamable.isIgnited())
			Ignite();
	}
	
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.GetComponent<Substance>() != null && col.gameObject.GetComponent<Substance>().isWater())
		{
			Extinguish();
		}
		if(col.gameObject.GetComponent("Flamable") == null)
		{
			return;	
		}
		
		Flamable other_flamable = col.gameObject.GetComponent("Flamable") as Flamable;
		
		if(other_flamable.isIgnited())
			Ignite();
	}
}