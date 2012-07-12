using UnityEngine;
using System.Collections;

public class QuestChecker {
	public bool checkIfCompleted()
	{
		GameObject crate1 = GameObject.Find("QuestFlammingCrate");
		GameObject crate2 = GameObject.Find("QuestFlammingCrate1");
		
		if((crate1.GetComponent("Flamable") as Flamable).isIgnited() && (crate1.GetComponent("Flamable") as Flamable).isIgnited())
			return true;
		
		return false;
	}
}