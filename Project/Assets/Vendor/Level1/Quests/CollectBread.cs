using UnityEngine;
using System.Collections;

public class CollectBread : MonoBehaviour {
	public delegate void EventHandler();
	public static event EventHandler CollectedBread;
	
	void OnTriggerEnter(Collider col)
	{
	    if(col.gameObject.tag.Equals("Player"))
	    {
			Destroy(this.gameObject);
			CollectedBread();
	    }
	}
}