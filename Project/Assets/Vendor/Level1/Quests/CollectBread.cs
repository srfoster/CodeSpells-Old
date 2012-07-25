using UnityEngine;
using System.Collections;

public class CollectBread : MonoBehaviour {
	public delegate void EventHandler();
	public static event EventHandler CollectedBread;
	
	void OnControllerColliderHit(Collision col)
	{
	    if(col.gameObject.tag == "Player")
	    {
		    Debug.Log("Destroying");
			Destroy(this.gameObject);
			Debug.Log("collecting bread!");
			CollectedBread();
	    }
	}
}