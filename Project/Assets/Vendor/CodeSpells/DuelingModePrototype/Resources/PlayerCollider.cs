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
		decreaseHealth(hitDamage, col.gameObject);
		//crateCollisionTimer = new System.Timers.Timer();
		//crateCollisionTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}
	
	void OnCollisionEnter(Collision col) {
		//start timed event
		//if (col.gameObject().equals ())
		Debug.Log ("Inside OnColliderEnter" + col.gameObject);
		decreaseHealth(hitDamage, col.gameObject);
		//crateCollisionTimer = new System.Timers.Timer();
		//crateCollisionTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}
	
	
	void OnCollisionExit(Collision col) {
		//end timed event
	}
	
	
	void decreaseHealth(double amount, GameObject gameObject) {
		if(gameObject.GetComponent<Health>() != null && gameObject.GetComponent<ExampleDisplayHealth>().heart_texture.name == heartColor)
			gameObject.GetComponent<Health>().decreaseHealth(10.0);
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
