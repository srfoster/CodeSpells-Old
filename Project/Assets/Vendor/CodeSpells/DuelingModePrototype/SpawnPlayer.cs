using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

	public GameObject to_spawn;
	
	GameObject actual;
	
	// Use this for initialization
	void Start () {
	//	actual = Instantiate(to_spawn, transform.position, transform.rotation) as GameObject;
	}
	
	
	void Spawn()
	{
		//Destroy(actual);
		
		
		
		actual = Network.Instantiate(to_spawn, transform.position, transform.rotation, 0) as GameObject;
		
		
	}
}
