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
	
	void OnCollisionStay(Collision col)
	{
		if(!col.gameObject.name.Equals("Terrain"))
			Debug.Log(col.gameObject);
		if(col.gameObject.GetComponent("Flamable") == null)
		{
			return;	
		}
		
		Flamable other_flamable = col.gameObject.GetComponent("Flamable") as Flamable;
		
		if(other_flamable.isIgnited())
			Ignite();
	}
}
