using UnityEngine;
using System.Collections;

public class SummonObject : MonoBehaviour {
	public delegate void EventHandler();
	public static event EventHandler SummonObjectQuest;
	
	void OnTriggerEnter(Collider col)
	{
	    if(col.gameObject.name.Equals("SummonedObject"))
	    {
			if(SummonObjectQuest != null)
				SummonObjectQuest();
	    }
	}
}