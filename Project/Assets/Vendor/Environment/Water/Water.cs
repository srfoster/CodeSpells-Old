using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	
	static GameObject water_plane;
	GameObject player;
	
	void Start(){
		if(water_plane == null)
			water_plane = Instantiate(Resources.Load("WaterPlane") as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
		
		player = GameObject.Find("Main Camera");
	}
	
	void Update(){
			water_plane.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
			water_plane.transform.Translate(Vector3.forward * 3);
			water_plane.transform.rotation = player.transform.rotation;
	}
}
