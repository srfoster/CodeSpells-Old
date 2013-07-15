using UnityEngine;
using System.Collections;

public class UnlevitateUnlocker : QuestChecker {

	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		return badgebook.IsComplete("unlevitate");
	}
}