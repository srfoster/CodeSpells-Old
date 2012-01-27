using UnityEngine;
using System.Collections;

public class NPCAPI : MonoBehaviour {

	public void TurnTowardPlayer()
	{
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
	}
	
	void Update()
	{
		//TurnTowardPlayer();	
	}
}
