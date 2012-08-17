using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageMinimap : MonoBehaviour {
	
	private float inventoryWidth = 260f;
	//bool buttonPressed = false;
	//GameObject inv;
	
	/*void OnGUI() {
		//button to toggle inventory
		if (GUI.Button(new Rect(0,0,100,20), "toggle inventory"))
			buttonPressed = true;
		if(buttonPressed) {
			buttonPressed = false;
			if (inv.active)
				inv.active = false;
			else
				inv.active = true;
		}
	}*/
	
	
	void Start() {
		//inv = GameObject.Find ("Inventory");
		resizeMap();
	}

	void resizeMap() {
		camera.depth = 2; //make visible, X,Y,W,H
		camera.rect = new Rect(1-inventoryWidth/Screen.width,0.065f, (inventoryWidth-42)/Screen.width,0.31f);
	}
}


