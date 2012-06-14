using UnityEngine;
using System.Collections;

public class CollisionAction : MonoBehaviour {
	private double lastRotY = 0.0;
	private bool counterClockwise = true;
	private int counter = 0;
	
	void Start() {		
		transform.rotation = Quaternion.identity;
	}
	void Update() {
		if (transform.rotation.y > lastRotY) {
			//Debug.Log ("counter clockwise");
		}
		else {
			//Debug.Log ("clockwise");
		}
		counterClockwise = (transform.rotation.y > lastRotY);
	}
	
	void OnControllerColliderHit(ControllerColliderHit col)
	{

		return;
		
		if(col.gameObject.tag.Equals("InanimateObject"))
		{
			counter++;
			Debug.Log ("counter: "+counter);
			if(counterClockwise) {
				transform.Rotate(0.0f, transform.rotation.y+5.0f, 0.0f);
			}
			else {
				transform.Rotate(0.0f, transform.rotation.y-5.0f, 0.0f);
			}
		}
	}
}
