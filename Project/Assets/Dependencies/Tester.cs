using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {

	// Use this for initialization
	int value;
	void Start () {
		value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		value += 1;
		//Debug.Log ("value is now "+value);
	}
}
