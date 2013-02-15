using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {

	private bool shakeMode = false;
	private bool up = true;
	
	// Use this for initialization
	public void startShakeMode() {
		//Start Shaking
		shakeMode = true;
	}
	
	public void endShakeMode() {
		//Stop Shaking
		shakeMode = false;
	}
	
	// Update is called once per frame
	public void UpdateShake() {
		if (shakeMode) {
			transform.position = new Vector3(transform.position.x, transform.position.y + (up ? 0.07f : -0.07f), transform.position.z);
			up = !up;
		}
	}
}
