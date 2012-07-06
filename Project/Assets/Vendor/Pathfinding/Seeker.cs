using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Seeker : MonoBehaviour {
	public enum WalkingState {NotStarted=1, OnPath, ReachedDestination, UnreachablePosition};
	private GameObject destination;
	private List<Vector3> movements;
	private int pathIndex = 1;
	private bool hasCalled = false;
	private WalkingState currentState = WalkingState.NotStarted;
	
	void Start() {
		
	}
	
	public WalkingState getState() {
		return currentState;
	}
	
	public void setPath(List<Vector3> vectorList) {
		movements = vectorList;
		pathIndex = 1;
	}
	
	public void setDestination(GameObject dest) {
		// Finds all the movements to the final destination
		movements = (GameObject.Find ("PathsThroughGame").GetComponent<Navigation>()).findPath(transform.position, dest.transform.position);
		pathIndex = 1;
		currentState = WalkingState.OnPath;
	}
	
	public WalkingState walk() { //code to walk
		if(pathIndex < movements.Count-1)
		{
			NPCFidget fidgets = GetComponent<NPCFidget>();
	   		fidgets.StartWalking();
			
			transform.LookAt (movements[pathIndex]);
			transform.Translate(Vector3.forward * Time.deltaTime * 3);
			transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
			
			if (Vector3.Distance(transform.position, movements[pathIndex]) < 1) {
				pathIndex++;
			}
			return currentState;
		}
		
		currentState = WalkingState.NotStarted;
		return WalkingState.ReachedDestination;
	}
}



