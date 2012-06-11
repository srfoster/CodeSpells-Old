using UnityEngine;
using System.Collections;

public abstract class GnomeAI : MonoBehaviour {
	enum ActionState {Find=1, Collect, Walk, Deliver, Back, Eat};
	bool working = false;
	ActionState currentState;
	
	// Use this for initialization
	void Start () {
		currentState = ActionState.Walk;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Update is being called and my state is: "+currentState);
		switch(currentState)
		{
			case ActionState.Find: 	
				if(Find())
					currentState = ActionState.Collect;
				break;
			case ActionState.Collect: 	
				if(Collect())
					currentState = ActionState.Walk;
				break;
			case ActionState.Walk: 	
				Debug.Log("I get to try to walk!");
				if(Walk())
				{
					Debug.Log("I'm changing my state");
					currentState = ActionState.Deliver;
				}
				else{
					Debug.Log("I'm not changing my state");
				}
				break;
			case ActionState.Deliver: 	
				if(Deliver())
					currentState = ActionState.Back;
				break;
			case ActionState.Back: 	
				Debug.Log("I get to try to go back!");
				if(Back())
					currentState = ActionState.Eat;
				break;
			case ActionState.Eat: 	
				if(Eat())
					currentState = ActionState.Find;
				break;
			default:
				break;
		}
	}
	
	public abstract bool Find();
	public abstract bool Collect();
	public abstract bool Walk();
	public abstract bool Deliver();
	public abstract bool Back();
	public abstract bool Eat();
}
