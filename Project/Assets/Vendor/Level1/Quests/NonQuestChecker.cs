using UnityEngine;
using System.Collections;

public class NonQuestChecker : QuestChecker {

	public override bool checkIfCompleted()
	{
		return false;
	}
}