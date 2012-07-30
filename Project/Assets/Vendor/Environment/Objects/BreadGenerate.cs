using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreadGenerate : MonoBehaviour {
	
	public GameObject prefab;
	
	public int number = 10;
	
	public int replace_if_below = 0;
	
	private List<GameObject> made_by_me = new List<GameObject>();
	
	void Start () {
		for(int i = 0; i < number; i++)
		{
			addObject();
		}
	}
	
	void Update(){
		while(shouldReplace() && playerFarAway())
		{
			addObject();
		}
	}
	
	void addObject(){
		GameObject obj = Instantiate(prefab, random_location(), Quaternion.identity) as GameObject;
		
		//For variety	
		obj.transform.RotateAround(transform.position, Vector3.up, random_angle()); // Spread them out
		
		made_by_me.Add(obj);	
	}
	
	bool playerFarAway(){
		return Vector3.Distance(transform.position, GameObject.Find("First Person Controller").transform.position) > 50;	
	}
	
	bool shouldReplace(){
		return numberNearby() < replace_if_below;	
	}
	
	int numberNearby(){
		Collider[] all_objects = Physics.OverlapSphere(transform.position, transform.localScale.x);
		
		List<GameObject> relevant_objects = new List<GameObject>();
		
		foreach(Collider col in all_objects)
		{
			if(made_by_me.Contains(col.gameObject))
			{
				relevant_objects.Add(col.gameObject);	
			}
		}
		
		return relevant_objects.Count;
	}
	
	Vector3 random_location()
	{
		float distance = Random.Range(0.0f, transform.localScale.x);
		
		
		Vector3 ret = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		ret.x += distance;
		
		return ret;
	}
	
	float random_angle()
	{
		return 360 * Random.value;
	}
}