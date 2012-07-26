using UnityEngine;
using System.Collections;

public class FlyQuestChecker : QuestChecker {
	public delegate void EventHandler();
	public static event EventHandler UnlockedStaff;
	public static event EventHandler Levitated;
	
	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		
		if(Levitated != null && !badgebook.IsComplete("helping_others_reaching_up_high"))
			Levitated();

		if(badgebook.IsComplete("helping_others_putting_something_high"))
		{
			if(UnlockedStaff != null)
				UnlockedStaff();
			return true;
		}
		return false;
	}
}