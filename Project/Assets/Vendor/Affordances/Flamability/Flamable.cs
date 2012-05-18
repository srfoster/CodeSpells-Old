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
	
	void Ignite()
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
		Debug.Log("I'm trying to extinguish something that is ignited");
		Destroy(flames_actual);
	}
	
	void OnCollisionStay(Collision col)
	{
		Debug.Log("I'm colliding with: "+col.gameObject);
		if(col.gameObject.GetComponent<Substance>() != null && col.gameObject.GetComponent<Substance>().isWater())
		{
			Debug.Log("I'm trying to extinguish something");
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
				Debug.Log("I'm colliding with: "+col.gameObject);
		if(col.gameObject.GetComponent<Substance>() != null && col.gameObject.GetComponent<Substance>().isWater())
		{
			Debug.Log("I'm trying to extinguish something");
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
