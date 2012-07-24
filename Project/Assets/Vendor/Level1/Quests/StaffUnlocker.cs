using UnityEngine;
using System.Collections;

public class StaffUnlocker : QuestChecker {

	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		return badgebook.IsComplete("collecting_objects_staff");
	}
}