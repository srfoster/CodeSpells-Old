using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class Health : MonoBehaviour {
	
	private double myHealth;
	//public enum HealthLevel {Dead, Sick, Unhealthy, ModeratelyHealthy, Healthy, VeryHealthy, Perfect};
	
	private System.Timers.Timer myTimer;
	private readonly double maxHealth = 100.0;
	private readonly double minHealth = 0.0;
	
	void Start () {
		myHealth = maxHealth;
		//starts timer with a 25 second interval
			myTimer = new System.Timers.Timer();
			myTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			myTimer.Interval = 25000;
			myTimer.Enabled = true;
		
	}
	
	private void OnTimedEvent(object source, ElapsedEventArgs e) {
		decreaseHealth(0.4);
    }
	
	public void createTimer(EventHandler eh, 
	
	
	
	/*HealthLevel getHealthState() {
		double diff = (myHealth - minHealth)/maxHealth;
		
		if (diff == 0.0) return HealthLevel.Dead;
		else if (diff < 0.15) return HealthLevel.Sick;
		else if (diff < 0.35) return HealthLevel.Unhealthy;
		else if (diff == 1.0) return HealthLevel.Perfect;
		else if (diff > 0.85) return HealthLevel.VeryHealthy;
		else if (diff > 0.65) return HealthLevel.Healthy;
		else return HealthLevel.ModeratelyHealthy;		
			
	}*/
	
	double getHealth() {
		return myHealth;
	}
	
	string percentOfTotalHealth() {
		return Math.Round((myHealth - minHealth)/maxHealth*100.0, 1)+"%";
	}
	
	void decreaseHealth(double dec) {
		Debug.Log ("decreaseHealth was called, "+DateTime.Now.ToString("HH:mm:ss tt")+", health is: "+percentOfTotalHealth());
		if (myHealth - dec < minHealth) {
			myHealth = minHealth;
		}
		else
			myHealth -= dec;
	}
	
	void increaseHealth(double inc) {
		if (myHealth + inc > maxHealth) {
			myHealth = maxHealth;
		}
		else
			myHealth += inc;
	}
}
