using UnityEngine;
using System.Collections;

public class Waterable : MonoBehaviour {
	public bool isGrowing = false;
	public float maxSize;
	
	// If it's being watered, grow
	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.tag.Equals("Water"))
		{
			isGrowing = true;
		}
	}
	
	void Update()
	{
		if(isGrowing && (transform.localScale.x < maxSize))
		{
			transform.localScale = new Vector3(transform.localScale.x + 0.01f, 
											transform.localScale.y + 0.01f, 
											transform.localScale.z + 0.01f);	
		}
		else if(isGrowing && (transform.localScale.x >= maxSize))
		{
			Vector3 flowerLocation = transform.position;
			
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if(collider.gameObject.tag.Equals("Water"))
		{
			isGrowing = false;
		}
	}
}
