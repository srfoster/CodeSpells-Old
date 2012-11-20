using UnityEngine;
using System.Collections;

public class EnterPracticeRoom : MonoBehaviour {

	// Use this for initialization
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			Debug.Log ("Loading level 1");
			Application.LoadLevel(1);
		}
	}
}