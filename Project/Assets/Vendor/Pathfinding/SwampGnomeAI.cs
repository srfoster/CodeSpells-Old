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
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(FromObj);
		
		GetComponent<Seeker>().walk();
		
		return (GetComponent<Seeker>().getState() == Seeker.WalkingState.ReachedDestination);
	}
	
	public override bool Eat()
	{
		return true;
	}
	
	public override bool Walk()
	{
		Debug.Log("I'm walking in my AI");
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(ToObj);
		
		GetComponent<Seeker>().walk();
		
		Debug.Log("I'm returning True or False from my AI walk: "+ (GetComponent<Seeker>().getState() == Seeker.WalkingState.ReachedDestination));
		
		return (GetComponent<Seeker>().getState() == Seeker.WalkingState.ReachedDestination);
	}
}
