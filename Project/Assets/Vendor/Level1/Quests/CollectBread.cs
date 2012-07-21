using UnityEngine;
using System.Collections;

public class CollectBread : MonoBehaviour {

	void onTriggerEnter(Collider col)
	{
		if(!col.gameObject.name.Equals("Player"))
			return;
		
		
	}
}
