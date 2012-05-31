using UnityEngine;
using System.Collections;

public abstract class GnomeAI : MonoBehaviour {
	enum State {Find=1, Collect, Walk, Deliver, Back, Eat};
	bool working = false;
	State currentState;
	
	// Use this for initialization
	void Start () {
		currentState = State.Walk;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
		{
			case State.Find: 	
				if(Find())
					currentState = State.Collect;
				break;
			case State.Collect: 	
				if(Collect())
					currentState = State.Walk;
				break;
			case State.Walk: 	
				if(Walk())
					currentState = State.Deliver;
				break;
			case State.Deliver: 	
				if(Deliver())
					currentState = State.Back;
				break;
			case State.Back: 	
				if(Back())
					currentState = State.Eat;
				break;
			case State.Eat: 	
				if(Eat())
					currentState = State.Find;
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
