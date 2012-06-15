using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageMinimap : MonoBehaviour {
	
	private int screenHeight = 0;
	private int screenWidth = 0;
	//private int mapWidthPixels = 150;
	//private int mapHeightPixels = 100;
	private int mapWidthPixels = 300;
	private int mapHeightPixels = 200;
	private int counter = 15;

	void Update() {	

		if ((Screen.height != screenHeight) || (Screen.width != screenWidth)) {
			resizeMap ();
			screenHeight = Screen.height;
			screenWidth = Screen.width;
			
		}
	}
	
	void resizeMap() {
		camera.depth = 2; //make visible, X,Y,W,H
		camera.rect = new Rect(0.0f, 0.0f, (float)((mapWidthPixels+0.0)/Screen.width), (float)((mapHeightPixels+0.0)/Screen.height));
		//camera.rect = new Rect((float)(1-(mapWidthPixels+0.0)/Screen.width), (float)(1-(mapHeightPixels+0.0)/Screen.height), 1.0f, 1.0f);
	}
}


