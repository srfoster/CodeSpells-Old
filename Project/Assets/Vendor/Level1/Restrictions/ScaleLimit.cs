using UnityEngine;
using System.Collections;

public class ScaleLimit : MonoBehaviour {
	
	Vector3 oldScale;
	
	public float lower_limit = 1;

	public float upper_limit = 100;
	
	void Start(){
		oldScale = transform.localScale;	
	}

	// Update is called once per frame
	void LateUpdate () {
				
		float volume = collider.bounds.size.x * collider.bounds.size.y * collider.bounds.size.z;
		
		//We don't want the object to 'flip' -- i.e. it get so small that it starts getting big again.
		bool neg = transform.localScale.x < 0 ||  transform.localScale.y < 0 || transform.localScale.z < 0;
		
		if(volume > upper_limit || volume < lower_limit || neg)
			transform.localScale = oldScale;
		
		oldScale = transform.localScale;
	}
}
