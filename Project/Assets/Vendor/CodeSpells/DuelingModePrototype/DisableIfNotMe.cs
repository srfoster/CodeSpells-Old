using UnityEngine;
using System.Collections;

public class DisableIfNotMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(!networkView.isMine)
		{
			Destroy(GetComponent<MyFPSInputController>());	
		}
	}
	
}
