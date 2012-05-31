using UnityEngine;
using System.Collections;

public class SwampGnomeAI : GnomeAI {

	public GameObject ToObj;
	public GameObject FromObj;
	
	public override bool Find()
	{
		return true;
	}
	
	public override bool Collect()
	{
		return true;
	}

	public override bool Deliver()
	{
		return true;
	}
	
	public override bool Back()
	{
	   	movements.StartWalking();
		
	   	Vector3 destination = FromObj.transform.position;
		
	   	transform.LookAt(destination);
	   	transform.Translate(Vector3.forward * Time.deltaTime);
		
	  	transform.position = new Vector3(transform.position.x,
		Terrain.activeTerrain.SampleHeight(transform.position),
		transform.position.z);
		
		float dist = Vector3.Distance(FromObj.transform.position, transform.position);
		return (dist < 3);
	}
	
	public override bool Eat()
	{
		return true;
	}
	
	public override bool Walk()
	{
		NPCFidget movements = GetComponent<NPCFidget>();
	   	movements.StartWalking();
		
	   	Vector3 destination = ToObj.transform.position;
		
	   	transform.LookAt(destination);
	   	transform.Translate(Vector3.forward * Time.deltaTime);
		
	  	transform.position = new Vector3(transform.position.x,
		Terrain.activeTerrain.SampleHeight(transform.position),
		transform.position.z);
		
		float dist = Vector3.Distance(ToObj.transform.position, transform.position);
		return (dist < 3);
	}
}
