using UnityEngine;
using System.Collections;

public class FireQuestChecker : QuestChecker {

	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		return badgebook.IsComplete("helping_others_light_fire");
	}
}