using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class BouncingBread : MonoBehaviour {
	
	int count = 0;
	
	// Update is called once per frame
	void Update () {		
		transform.position = new Vector3(transform.position.x,
			transform.position.y + bounce(),
			transform.position.z);
	}
	
	float bounce()
	{
		return Mathf.Sin((count++)/2) / 5;
	}
}
