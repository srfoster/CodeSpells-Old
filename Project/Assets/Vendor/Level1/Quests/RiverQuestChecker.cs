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
			if(!badgebook.Contains("completed_helping_others_cross_river"))
				badgebook.Replace("helping_others_cross_river", "completed_helping_others_cross_river", "  Cross River [COMPLETED]", "test_badge", true);
			return true;
		}
		
		return false;
	}
}