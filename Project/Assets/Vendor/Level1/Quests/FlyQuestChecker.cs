using UnityEngine;
using System.Collections;

public class FlyQuestChecker : QuestChecker {
	public delegate void EventHandler();
	public static event EventHandler UnlockedStaff;
	
	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		if(badgebook.IsComplete("helping_others_collecting_something_high"))
		{
			if(UnlockedStaff != null)
				UnlockedStaff();
			return true;
		}
		return false;
	}
}