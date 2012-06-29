using UnityEngine;
using System.Collections;

public class AvoidRiver : MonoBehaviour {
	
	public bool detect_jump = false;
	
	void OnTriggerEnter (Collider collider) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		// Make sure that the player was the one that collided with the boundary
		if(collider.gameObject != player)
			return;
		
		if(detect_jump)
		{
			if(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().IsJumping())
				GameObject.Find("Monster").GetComponent<Monster>().Attack();
		} else {
			GameObject.Find("Monster").GetComponent<Monster>().Attack();
		}
	}

}
