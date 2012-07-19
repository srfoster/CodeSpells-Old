using UnityEngine;
using System.Collections;

public class ScaleLimit : MonoBehaviour {
	
	Vector3 oldScale;
	
	public float lower_limit = 0;

	public float upper_limit = 100;
	
	void Start(){
		oldScale = transform.localScale;	
	}

	// Update is called once per frame
	void LateUpdate () {
		//Debug.Log("Bounds " + collider.bounds);	
		
		
		float volume = collider.bounds.size.x * collider.bounds.size.y * collider.bounds.size.z;
		
		if(volume > upper_limit || volume < lower_limit)
			transform.localScale = oldScale;
		
		oldScale = transform.localScale;
	}
}
