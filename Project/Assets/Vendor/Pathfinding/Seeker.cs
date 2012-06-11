using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Seeker : MonoBehaviour {
	public enum WalkingState {NotStarted=1, OnPath, ReachedDestination, UnreachablePosition};
	private GameObject destination;
	private List<Vector3> movements;
	private int pathIndex = 0;
	private bool hasCalled = false;
	private WalkingState currentState = WalkingState.NotStarted;
	
	void Start() {
		
	}
	
	public WalkingState getState() {
		return currentState;
	}
	
	public void setPath(List<Vector3> vectorList) {
		movements = vectorList;
		pathIndex = 0;
	}
	
	public void setDestination(GameObject dest) {
		movements = (GameObject.Find ("PathsThroughGame").GetComponent<Navigation>()).findPath(transform.position, dest.transform.position);
		pathIndex = 0;
	}
	
	public void walk() { //code to walk
		Debug.Log("I'm walking in Seeker");
		if (pathIndex != movements.Count) {
			NPCFidget fidgets = GetComponent<NPCFidget>();
	   		fidgets.StartWalking();
			Debug.Log("I'm walking to the next waypoint which is: "+movements[pathIndex]);
			transform.LookAt (movements[pathIndex]);
			transform.Translate(5*Vector3.forward * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
			
			if (Vector3.Distance(transform.position, movements[pathIndex]) < 3) {
				pathIndex++;
			}
		}
		else 
			currentState = WalkingState.ReachedDestination;
	}
}



