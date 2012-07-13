using UnityEngine;
using System.Collections;

public class FireQuestChecker : QuestChecker {

	public override bool checkIfCompleted()
	{
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		GameObject crate1 = GameObject.Find("QuestFlammingCrate");
		GameObject crate2 = GameObject.Find("QuestFlammingCrate1");
		
		if((crate1.GetComponent("Flamable") as Flamable).isIgnited() && (crate1.GetComponent("Flamable") as Flamable).isIgnited())
		{
			if(!badgebook.Contains("complete_helping_others_light_fire"))
				badgebook.Complete("helping_others_light_fire");
			return true;
		}
		return false;
	}
}