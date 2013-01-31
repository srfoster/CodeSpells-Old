using UnityEngine;
using System.Collections;

public class RestartPracticeRoom : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("Gnome"))
		{
			GameObject start = GameObject.FindGameObjectWithTag("Respawn");
			
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.transform.position = start.transform.position;
			player.transform.rotation = start.transform.rotation;
			
			// Note: Setting the gnome's rotation makes it twist its head backward and it looks scary, so I took that out
			GameObject gnome = GameObject.FindGameObjectWithTag("Gnome");
			Vector3 newPosition = new Vector3(start.transform.position.x + 2, start.transform.position.y - 2, start.transform.position.z);
			gnome.transform.position = newPosition;	
			
			Debug.Log("About to make ones of the walls disappear");
			GameObject[] ifGates;
        	ifGates = GameObject.FindGameObjectsWithTag("IfGate");
        	foreach (GameObject ifGate in ifGates) {
				Debug.Log("Woo hoo! chose a wall!");
            	RandomDoor rD = ifGate.GetComponent<RandomDoor>();
				rD.changeDoor();
        	}
		}
	}
}
