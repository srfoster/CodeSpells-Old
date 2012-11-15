using UnityEngine;
using System.Collections;

public class LeavePracticeRoom : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag.Equals("Player"))
			Application.LoadLevel(0);
	}
}
