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
		
		//Destroy(GameObject.Find("Main Camera"));
		
		actual = Network.Instantiate(to_spawn, transform.position, transform.rotation, 0) as GameObject;
		
		GameObject camera_prefab = Resources.Load("MainCamera") as GameObject;
		GameObject minimap_camera_prefab = Resources.Load("MinimapCamera") as GameObject;
		
		GameObject camera = Instantiate(camera_prefab, actual.transform.position, actual.transform.rotation) as GameObject;
		GameObject minimap_camera = Instantiate(minimap_camera_prefab, actual.transform.position, Quaternion.identity) as GameObject;

		camera.transform.parent = actual.transform;
		minimap_camera.transform.parent = actual.transform;
		
		camera.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y + 1.79f,camera.transform.position.z);
		
		minimap_camera.transform.position = new Vector3(minimap_camera.transform.position.x,minimap_camera.transform.position.y + 75f,minimap_camera.transform.position.z);
		minimap_camera.transform.rotation = Quaternion.Euler(90f,0f,0f);
	}
}
