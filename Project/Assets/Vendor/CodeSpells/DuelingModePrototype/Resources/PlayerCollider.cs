using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {
	
	public System.Timers.Timer crateCollisionTimer;
	public int timeInterval;
	public double hitDamage;
	
	void OnColliderEnter(Collision col) {
		//start timed event
		//if (col.gameObject().equals ())
		decreaseHealth(hitDamage, col);
		//crateCollisionTimer = new System.Timers.Timer();
		//crateCollisionTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
	}
	
	
	void OnColliderExit(Collision col) {
		//end timed event
	}
	
	
	void decreaseHealth(double amount, Collision col) {
		if(col.gameObject.GetComponent<Health>() != null)
			col.gameObject.GetComponent<Health>().decreaseHealth(amount);
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
