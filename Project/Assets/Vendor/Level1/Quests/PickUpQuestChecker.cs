using UnityEngine;
using System.Collections;

public class PickUpQuestChecker : QuestChecker {
 
	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		GameObject presents = GameObject.Find("Presents");
		if((presents.GetComponent("PickUpableItem") as PickUpableItem).isInInventory())
		{
			//Unlocked the pick up items badge for the first time
			if(!badgebook.Contains("completed_helping_others_pick_up_item"))
			{
				badgebook.Replace("helping_others_pick_up_item", "completed_helping_others_pick_up_item", "  Pick Up Item [COMPLETED]", "test_badge", true);
				return true;
			}

			else if(badgebook.Contains("completed_helping_others_pick_up_item") && badgebook.Contains("completed_helping_others_cross_river"))
			{
				return true;
			}
		}
		return false;
	}
}