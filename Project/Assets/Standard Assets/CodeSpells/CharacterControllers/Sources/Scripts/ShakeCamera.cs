using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	private bool shakeMode = false;
	private bool up = true;
	private Vector3 myPos;
	// Use this for initialization
	public void startShakeMode() {
		myPos = transform.position;
		shakeMode = true;
	}
	
	public void endShakeMode() {
		//resets position
		shakeMode = false;
		transform.position = myPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeMode) { //
			transform.position = new Vector3(transform.position.x, transform.position.y + (up ? 0.07f : -0.07f), transform.position.z);
			up = !up;
		}
	}
}
