using UnityEngine;
using System.Collections;

public class FlowerGnomeAI : GnomeAI {

	public GameObject ToObj;
	public GameObject FromObj;
	private GameObject objToCollect;
	private Transform leftHand = null;
	private Transform rightHand = null;
	private bool armsUp = false;
	private bool foundObject = false;
	private float walkingSpeed = 2;
	
	public override bool Find()
	{
		if(!foundObject)
		{
			// Find all flour within the whole game
			foreach( GameObject flour in GameObject.FindGameObjectsWithTag("Flour"))
			{
				//If flour exists, and it hasn't been collected yet, then the gnome should collect it
				if(flour != null && flour.transform.parent == null && Vector3.Distance(transform.position, flour.transform.position) <= 60)
				{
					objToCollect = flour;
					objToCollect.tag = "Untagged";
					foundObject = true;
					break;
				}
			}
		}
		else
		{
			return walkToObject();
		}
		return false;
	}
	
	private bool walkToObject()
	{
		transform.LookAt (objToCollect.transform);
		transform.Translate(Vector3.forward * Time.deltaTime * walkingSpeed);
		transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
		return (Vector3.Distance(transform.position, objToCollect.transform.position) < 2);
	}
	
	public override bool Collect()
	{
		foundObject = false;
		leftHand = findLeftHandRecursive(transform);
		rightHand = findRightHandRecursive(transform);
		if(leftHand != null && rightHand != null)
		{
			armsUp = true;
			objToCollect.rigidbody.active = false;
			objToCollect.transform.position = transform.position + transform.forward*2;
			objToCollect.transform.position = new Vector3(objToCollect.transform.position.x, objToCollect.transform.position.y+1, objToCollect.transform.position.z);
			objToCollect.transform.parent = transform;
			return true;
		}
		return false;
	}
	
	public void LateUpdate()
	{
		if(armsUp)
		{
			leftHand.Rotate(-90, 0, 0);
			rightHand.Rotate(90, 0, 0);
		}
	}
	
	private Transform findLeftHandRecursive(Transform parent)
	{
		Transform ret;
		if(parent.name.Equals("shouder_L"))
		{
			return parent;
		}
		
		foreach(Transform child in parent)
		{
			ret = findLeftHandRecursive(child);
			if(ret != null)
				return ret;
		}
		return null;
	}
	
	private Transform findRightHandRecursive(Transform parent)
	{
		Transform ret;
		if(parent.name.Equals("shouder_R"))
		{
			return parent;
		}
		
		foreach(Transform child in parent)
		{
			ret = findRightHandRecursive(child);
			if(ret != null)
				return ret;
		}
		return null;
	}

	public override bool Deliver()
	{
		//If flour exists, and it is mine then release it
		if(objToCollect != null && objToCollect.transform.parent == transform)
		{
			objToCollect.transform.parent = null;
			armsUp = false;
			return true;
		}
		return false;
	}
	
	public override bool Back()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(FromObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		return (state == Seeker.WalkingState.ReachedDestination);
	}
	
	public override bool Eat()
	{
		return true;
	}
	
	public override bool Walk()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(ToObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		
		return (state == Seeker.WalkingState.ReachedDestination);
	}
}
