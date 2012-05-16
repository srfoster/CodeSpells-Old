using UnityEngine;
using System.Collections;

public class TestCarriable : MonoBehaviour {
	
	bool being_carried = false;
	
	bool skip = true;
	
	void OnMouseDown()
	{
		Debug.Log("HERE");
		Transform player = GameObject.FindGameObjectWithTag("MainCamera").transform;
		if(!being_carried)
		{
			transform.parent = player;
			being_carried = true;
			rigidbody.isKinematic = true;
			skip = true;
		}
	}
	
	void Update()
	{
		Transform player = GameObject.FindGameObjectWithTag("MainCamera").transform;

		if(Input.GetMouseButton(0) && being_carried && !skip)
		{
			transform.parent = null;
			being_carried = false;	
			rigidbody.isKinematic = false;
			rigidbody.AddForce(player.forward * 10, ForceMode.VelocityChange);
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			skip = false;
		}
	}
}
