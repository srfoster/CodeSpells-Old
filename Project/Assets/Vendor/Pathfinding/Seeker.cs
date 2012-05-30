using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

enum State {NotStarted, OnPath, ReachedDestination, UnreachablePosition};

public class Seeker : MonoBehaviour {
	public GameObject destination;
	private List<Vector3> movements;
	private int pathIndex = 0;
	private bool hasCalled = false;
	private Enum currentState = State.NotStarted;
	
	void Start() {
		
	}
	
	Enum getState() {
		return currentState;
	}
	
	void setPath(List<Vector3> vectorList) {
		movements = vectorList;
		pathIndex = 0;
	}
	
	void setDestination(GameObject dest) {
		movements = (GameObject.Find ("GameObjectTest").GetComponent<Navigation>()).findPath(transform.position, dest.transform.position);
		pathIndex = 0;
	}
	
	void Update() { //code to walk
		if (!hasCalled) {
			movements = (GameObject.Find ("GameObjectTest").GetComponent<Navigation>()).findPath(transform.position, destination.transform.position);
			hasCalled = true;
			if (movements.Count == 0) 
				currentState = State.UnreachablePosition;
			else 
				currentState = State.OnPath;
		}
		if (pathIndex != movements.Count) {
			transform.LookAt (movements[pathIndex]);
			transform.Translate(5*Vector3.forward * Time.deltaTime);
			if (Vector3.Distance(transform.position, movements[pathIndex]) < 10) {
				pathIndex++;
			}
		}
		else 
			currentState = State.ReachedDestination;
	}
}



