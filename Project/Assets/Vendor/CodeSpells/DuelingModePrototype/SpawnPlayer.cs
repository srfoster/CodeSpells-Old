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
		
		
		
		GameObject sample_crate_prefab = Resources.Load("sample_crate") as GameObject;
		
		Network.Instantiate(sample_crate_prefab, transform.position, Quaternion.identity, 0);
		
		
		actual = Network.Instantiate(to_spawn, transform.position, transform.rotation, 0) as GameObject;
		actual.name = "First Person Controller";
		
		GameObject camera_prefab = Resources.Load("MainCamera") as GameObject;
		GameObject minimap_camera_prefab = Resources.Load("MinimapCamera") as GameObject;
		
		GameObject camera = Instantiate(camera_prefab, actual.transform.position, actual.transform.rotation) as GameObject;
		camera.name = "Main Camera";
		camera.tag  = "MainCamera";
		GameObject minimap_camera = Instantiate(minimap_camera_prefab, actual.transform.position, Quaternion.identity) as GameObject;

		camera.transform.parent = actual.transform;
		minimap_camera.transform.parent = actual.transform;
		
		camera.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y + 1.79f,camera.transform.position.z);
		
		minimap_camera.transform.position = new Vector3(minimap_camera.transform.position.x,minimap_camera.transform.position.y + 75f,minimap_camera.transform.position.z);
		minimap_camera.transform.rotation = Quaternion.Euler(90f,0f,0f);
	}
}
