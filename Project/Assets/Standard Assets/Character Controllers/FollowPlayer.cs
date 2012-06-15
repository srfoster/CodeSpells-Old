using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour {
	
	public Transform mapTarget;
	private int screenHeight = 0;
	private int screenWidth = 0;
	private int mapWidthPixels = 150;
	private int mapHeightPixels = 100;
	private int counter = 15;

	void Update() {
		Debug.Log ("x: "+transform.position.x+" y: "+transform.position.y
			+" z: "+transform.position.z);
		Debug.Log ("Enters");
		//if(counter == 15) {
			//mapTarget.transform.position = new Vector3(transform.position.x, 75, transform.position.z);
			//counter = 0;
		//}
			

		if ((Screen.height != screenHeight) || (Screen.width != screenWidth)) {
			resizeMap ();
			//counter++;
			screenHeight = Screen.height;
			screenWidth = Screen.width;
			
		}
		//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		//counter++;
		}
	
	void resizeMap() {
		mapTarget.camera.depth = 2; //make visible, X,Y,W,H
		mapTarget.camera.rect = new Rect((float)(1-(mapWidthPixels+0.0)/Screen.width), (float)(1-(mapHeightPixels+0.0)/Screen.height), 1.0f, 1.0f);
	}
}


