using UnityEngine;
using System.Collections;

public class AdjustRainHeight : MonoBehaviour {
	float initial_height;
	
	void Start()
	{
		initial_height = transform.position.y;	
	}
	
	// This is purely to resist spells that would lower the rain cloud to ground level or below.
	void LateUpdate () {
		transform.position = new Vector3(transform.position.x, initial_height, transform.position.z);
	}
}
