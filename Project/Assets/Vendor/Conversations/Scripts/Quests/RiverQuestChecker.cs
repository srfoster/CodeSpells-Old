using UnityEngine;
using System.Collections;

public class RiverQuestChecker : QuestChecker {

	public override bool checkIfCompleted()
	{
		//PlayerAttributes player = new PlayerAttributes();
		//return player.isWestOfRiver();
		return false;
	}
}
