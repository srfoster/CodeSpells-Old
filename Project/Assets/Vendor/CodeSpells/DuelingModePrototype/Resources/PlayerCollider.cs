using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {
	
	public System.Timers.Timer crateCollisionTimer;
	public int timeInterval;
	public double hitDamage;
	public string heartColor;
	
	
	void OnTriggerEnter(Collider col) {
		//start timed event
		//if (col.gameObject().equals ())
		Debug.Log ("Inside OnColliderEnter" + col.gameObject);
		decreaseHealth(hitDamage, col);
		//crateCollisionTimer = new System.Timers.Timer();
		//crateCollisionTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}
	
	
	void OnCollisionExit(Collision col) {
		//end timed event
	}
	
	
	void decreaseHealth(double amount, Collider col) {
		if(col.gameObject.GetComponent<Health>() != null && col.gameObject.GetComponent<ExampleDisplayHealth>().heart_texture.name == heartColor)
<<<<<<< HEAD
			col.gameObject.GetComponent<Health>().decreaseHealth(10);
=======
			col.gameObject.GetComponent<Health>().decreaseHealth(10.0);
>>>>>>> fa6c8745b53ebc2161a27c6e6203f7297f0efe7e
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
