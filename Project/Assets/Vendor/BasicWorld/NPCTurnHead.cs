using UnityEngine;
using System.Collections;

public class NPCTurnHead : MonoBehaviour {
	
	private Transform player;
	private Transform head;
	
	private Vector3 last_direction;
	
	void Start (){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		findHeadRecursive(transform);
	}
	              
	void LateUpdate () {
		
		Vector3 forward = head.forward;
		Vector3 direction = player.position - head.position;
		
		float angle = Vector3.Angle(forward,direction);
		
		
		if(angle < 70)
		{
			head.LookAt(player, Vector3.left);
			last_direction = direction;
		} else {
		}
	}
	
	
	void findHeadRecursive(Transform parent)
	{
		if(parent.name.Equals("Head"))
		{
			head = parent;
		}
		
		foreach(Transform child in parent)
		{
			findHeadRecursive(child);
		}
	}
}
