using UnityEngine;
using System.Collections;

public class NPCTestWalk : MonoBehaviour {
	
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
	   NPCFidget movements = GetComponent<NPCFidget>();
	   movements.StartWalking();
		
	   Vector3 destination = GameObject.FindWithTag("Player").transform.position;
		
	   transform.LookAt(destination);
	   transform.Translate(Vector3.forward * Time.deltaTime);
		
	   transform.position = new Vector3(transform.position.x,
			Terrain.activeTerrain.SampleHeight(transform.position),
			transform.position.z);
	}
}
