using UnityEngine;
using System.Collections;

public class PickUpQuestChecker : QuestChecker {
 
	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		return badgebook.IsComplete("helping_others_picking_up_item");
	}
}
