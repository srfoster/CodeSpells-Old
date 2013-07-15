using UnityEngine;
using System.Collections;

public class SummonObjectUnlocker : QuestChecker {

	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		return badgebook.IsComplete("summonObject");
	}
}