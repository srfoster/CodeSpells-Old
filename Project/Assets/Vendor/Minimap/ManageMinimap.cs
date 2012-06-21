using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageMinimap : MonoBehaviour {
	
	private int screenHeight = 0;
	private int screenWidth = 0;
	private int mapWidthPixels = 250;
	private int mapHeightPixels = 200;
	private int yPadding = 35;
	private int xPadding = 15;
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
		camera.rect = new Rect((float)(1-(mapWidthPixels+xPadding+ 0.0f)/Screen.width), 0.0f + ((yPadding + 0.0f) / Screen.height), (float)((mapWidthPixels+0.0)/Screen.width), (float)((mapHeightPixels+0.0)/Screen.height));
	}
}


