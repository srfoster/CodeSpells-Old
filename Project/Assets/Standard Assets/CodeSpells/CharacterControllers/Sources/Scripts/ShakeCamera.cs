using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	private bool shakeMode = false;
	private bool up = true;
	private Vector3 myPos;
	// Use this for initialization
	public void startShakeMode() {
		Debug.Log("set shake mode to true at position: "+transform.position);
		myPos = transform.position;
		Debug.Log("at starting shake mode, mypos = "+myPos);
		shakeMode = true;
	}
	
	public void endShakeMode() {
		//resets position
		shakeMode = false; 
		Debug.Log("returning to start position: "+myPos+" from position: "+transform.position);
		transform.position = new Vector3(transform.position.x, myPos.y, transform.position.z);
	}
	
	// Update is called once per frame
	public void UpdateShake() {
		Debug.Log("In update and shakeMode is: "+shakeMode);
		if (shakeMode) { //
			Debug.Log("shaking");
			transform.position = new Vector3(transform.position.x, transform.position.y + (up ? 0.07f : -0.07f), transform.position.z);
			up = !up;
		}
	}
}
