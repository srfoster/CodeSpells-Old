using UnityEngine;
using System.Collections;

public class FlowerGnomeAI : GnomeAI {

	public GameObject ToObj;
	public GameObject FromObj;
	
	public override bool Find()
	{
		// Find all flour within the whole game
		foreach( GameObject flour in GameObject.FindGameObjectsWithTag("Flour"))
		{
			//If flour exists, and it hasn't been collected yet, then the gnome should collect it
			if(flour != null && flour.transform.parent == null && Vector3.Distance(transform.position, flour.transform.position) <= 60)
			{
				flour.tag = "Untagged";
				flour.transform.parent = transform;
				return true;
			}
		}
		return false;
	}
	
	public override bool Collect()
	{
		return true;
	}

	public override bool Deliver()
	{
		if(transform.Find("Flour") != null)
		{
			GameObject flour = transform.Find("Flour").gameObject;
			
			//If flour exists, and it is mine then release it
			if(flour != null && flour.transform.parent == transform)
			{
				flour.transform.parent = null;
				return true;
			}
		}
		return false;
	}
	
	public override bool Back()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(FromObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		
		return (state == Seeker.WalkingState.ReachedDestination);
	}
	
	public override bool Eat()
	{
		return true;
	}
	
	public override bool Walk()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(ToObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		
		return (state == Seeker.WalkingState.ReachedDestination);
	}
}
