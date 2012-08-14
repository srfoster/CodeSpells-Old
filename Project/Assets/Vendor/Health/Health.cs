using System.Collections;
using System.Timers;
using System;
using UnityEngine;

public class Health : MonoBehaviour {
  public double myHealth;

  void Start () {
	myHealth = 10;
  }

  double getHealth() {
    return myHealth;
  }

  //string percentOfTotalHealth() {
    //return Math.Round((myHealth - minHealth)/maxHealth*100.0, 1)+"%";
  //}

  void decreaseHealth(double dec) {
   //implement
	myHealth -= dec;
  }

  void increaseHealth(double inc) {
   //implement
	myHealth += inc;
  }
	
  
}