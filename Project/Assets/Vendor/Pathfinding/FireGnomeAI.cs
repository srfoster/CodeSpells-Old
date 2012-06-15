using UnityEngine;
using System.Collections;

public class FireGnomeAI : GnomeAI {

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
