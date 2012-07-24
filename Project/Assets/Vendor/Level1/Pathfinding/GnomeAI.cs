using UnityEngine;
using System.Collections;

public class GnomeAI : MonoBehaviour {
	enum ActionState {Find=1, Collect, Walk, Deliver, Back, Eat};
	bool working = false;
	public int startState = 1;
	ActionState currentState;
	public GameObject ToObj;
	public GameObject FromObj;
	private GameObject objToCollect;
	private Transform leftHand = null;
	private Transform rightHand = null;
	private bool armsUp = false;
	private bool foundObject = false;
	private float walkingSpeed = 6;
	public string incomingTag = "";
	public string outgoingTag = "";
	
	// Use this for initialization
	void Start () {
		currentState = (ActionState) startState;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(transform.gameObject.name + " is " + currentState.ToString());
		
		
		if(currentState == ActionState.Find)
		{
			if(Find())
				currentState = ActionState.Collect;
		}
		else if(currentState == ActionState.Collect)
		{
			if(Collect())
				currentState = ActionState.Walk;
		}
		else if(currentState == ActionState.Walk)
		{
			if(Walk())
				currentState = ActionState.Deliver;
		}
		else if(currentState == ActionState.Deliver)
		{
			if(Deliver())
				currentState = ActionState.Back;
		}
		else if(currentState == ActionState.Back)
		{
			if(Back())
				currentState = ActionState.Eat;
		}
		else if(currentState == ActionState.Eat)
		{
			if(Eat())
				currentState = ActionState.Find;
		}
	}
	
	public bool Find()
	{
		if(!foundObject)
		{
			foreach( GameObject item in GameObject.FindGameObjectsWithTag(incomingTag))
			{
				if(item != null && Vector3.Distance(transform.position, item.transform.position) <= 60)
				{
					objToCollect = item;
					objToCollect.tag = "Untagged";
					foundObject = true;
					break;
				}
			}
			
			if(!foundObject)
				GetComponent<NPCFidget>().StopWalking();
		}
		else
		{
			return walkToObject();
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
	
	private bool walkToObject()
	{
		GetComponent<NPCFidget>().StartWalking();
			
		if(objToCollect == null || objToCollect.transform == null)
		{
			foundObject = false;
			currentState = ActionState.Find;
			return false;
		}
		transform.LookAt (objToCollect.transform);
		transform.Translate(Vector3.forward * Time.deltaTime * walkingSpeed);
		transform.position = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
		return (Vector3.Distance(transform.position, objToCollect.transform.position) < 2);
	}
	
	public bool Collect()
	{
		GetComponent<NPCFidget>().StopWalking();

		foundObject = false;
		leftHand = findLeftHandRecursive(transform);
		rightHand = findRightHandRecursive(transform);
		if(leftHand != null && rightHand != null)
		{
			armsUp = true;
			if(objToCollect == null)
				currentState = ActionState.Find;
			objToCollect.transform.rotation = transform.rotation;
			objToCollect.transform.position = transform.position + transform.forward*2;
			objToCollect.transform.position = new Vector3(objToCollect.transform.position.x, objToCollect.transform.position.y+1, objToCollect.transform.position.z);
			objToCollect.transform.parent = transform;
			if(objToCollect.GetComponent("Evolvable") != null)
			{
				(objToCollect.GetComponent("Evolvable") as Evolvable).enabled = false;
			}
			
			if(objToCollect.GetComponent<PickUpableItem>() != null)
			{
				objToCollect.GetComponent<PickUpableItem>().enabled = false;
			}
			return true;
		}
		return false;
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

	public bool Deliver()
	{
		
		GetComponent<NPCFidget>().StopWalking();

		if(objToCollect != null && objToCollect.transform.parent == transform)
		{
			objToCollect.tag = outgoingTag;
			objToCollect.transform.parent = null;
			armsUp = false;
			if(objToCollect.GetComponent("Evolvable") != null)
			{
				(objToCollect.GetComponent("Evolvable") as Evolvable).enabled = true;
			}
			
			if(objToCollect.GetComponent<PickUpableItem>() != null)
			{
				objToCollect.GetComponent<PickUpableItem>().enabled = true;
			}
			return true;
		}
		return false;
	}
	
	public bool Back()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(FromObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		
		return (state == Seeker.WalkingState.ReachedDestination);
	}
	
	public bool Eat()
	{
		
		GetComponent<NPCFidget>().StopWalking();

		if(!incomingTag.Equals("Bread"))
		{
			foreach( GameObject item in GameObject.FindGameObjectsWithTag("Food"))
			{
				if(item != null && Vector3.Distance(transform.position, item.transform.position) <= 10)
				{
					Destroy(item);
					return true;
				}
			}
		}
		else
			return true;
		
		return false;
	}
	
	public bool Walk()
	{
		if(GetComponent<Seeker>().getState() == Seeker.WalkingState.NotStarted)
			GetComponent<Seeker>().setDestination(ToObj);
		
		Seeker.WalkingState state = GetComponent<Seeker>().walk();
		
		return (state == Seeker.WalkingState.ReachedDestination);
	}
}