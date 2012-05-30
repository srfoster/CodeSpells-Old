using UnityEngine;
using System.Collections;

public class GnomeAI : MonoBehaviour {
	enum State {Find=1, Found, Collected, Destination, Delivered, Eating};
	State currentState;
	
	// Use this for initialization
	void Start () {
		currentState = State.Find;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
