using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


public class NPCTurnHead : MonoBehaviour {
	
	private Transform player;
	private Transform head;
	
	
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
			head.LookAt(player, head.up);
		}
	}
	
	void findHeadRecursive(Transform parent)
	{
		if(Regex.Match(parent.name,"Head").Success)
		{
			head = parent;
			return;
		}
		
		foreach(Transform child in parent)
		{
			findHeadRecursive(child);
		}
	}
}
