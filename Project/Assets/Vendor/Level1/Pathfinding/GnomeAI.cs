using UnityEngine;
using System.Collections;

public abstract class GnomeAI : MonoBehaviour {
	enum ActionState {Find=1, Collect, Walk, Deliver, Back, Eat};
	bool working = false;
	public int startState = 1;
	ActionState currentState;
	
	// Use this for initialization
	void Start () {
		currentState = (ActionState) startState;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentState == ActionState.Find)
		{
			if(Find())
				currentState = ActionState.Collect;
		}
		else if(currentState == ActionState.Collect)
		{
			if(Collect())
				currentState = ActionState.Walk;
		}
		else if(currentState == ActionState.Walk)
		{
			if(Walk())
				currentState = ActionState.Deliver;
		}
		else if(currentState == ActionState.Deliver)
		{
			if(Deliver())
				currentState = ActionState.Back;
		}
		else if(currentState == ActionState.Back)
		{
			if(Back())
				currentState = ActionState.Eat;
		}
		else if(currentState == ActionState.Eat)
		{
			if(Eat())
				currentState = ActionState.Find;
		}
	}
	
	public abstract bool Find();
	public abstract bool Collect();
	public abstract bool Walk();
	public abstract bool Deliver();
	public abstract bool Back();
	public abstract bool Eat();
}
