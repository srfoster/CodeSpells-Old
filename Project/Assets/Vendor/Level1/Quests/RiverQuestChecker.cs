using UnityEngine;
using System.Collections;

public class RiverQuestChecker : QuestChecker {
	
	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		GameObject presents = GameObject.Find("Presents");
		GameObject gnome = GameObject.Find("QuestSwampGnomeReceive");
		if(Vector3.Distance(presents.transform.position, gnome.transform.position) < 5)
		{
			if(!badgebook.Contains("complete_helping_others_cross_river"))
				badgebook.Complete("helping_others_cross_river");
			return true;
		}
		
		return false;
	}
}
