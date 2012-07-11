using UnityEngine;
using System.Collections;

public class TestFirepit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider col)
	{
		
		if(col.gameObject.GetComponent<Flamable>() != null)
		{
			col.gameObject.GetComponent<Flamable>().Ignite();
		}
		
	}
}
