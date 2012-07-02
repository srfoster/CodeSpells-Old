using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {

	// Use this for initialization
	int value;
	void Start () {
		//create empty gameobject 4 units away
		//create new Vector3 with a dirrerent x position
		GameObject g = new GameObject();
		Instantiate (g, new Vector3(transform.position.x+2, transform.position.y, transform.position.z), Quaternion.identity);
		//g.transform.position.
		
		value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		value += 1;
		//Debug.Log ("value is now "+value);
	}
}
