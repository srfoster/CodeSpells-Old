using UnityEngine;
using System.Collections;

public class Unlevitate : MonoBehaviour {
	public delegate void EventHandler();
	public static event EventHandler UnlevitateQuest;
	private bool zero = false;
	private bool one = false;
	private bool two = false;
	
	void OnTriggerEnter(Collider col)
	{
	    if(col.gameObject.name.Equals("QuestUnlevitationCrate") && !zero)
	    {
			if(UnlevitateQuest != null)
			{
				zero = true;
				UnlevitateQuest();
			}
	    }
		else if(col.gameObject.name.Equals("QuestUnlevitationCrate1") && !one)
	    {
			if(UnlevitateQuest != null)
			{
				one = true;
				UnlevitateQuest();
			}
	    }
		else if(col.gameObject.name.Equals("QuestUnlevitationCrate2") && !two)
	    {
			if(UnlevitateQuest != null)
			{
				two = true;
				UnlevitateQuest();
			}
	    }
	}
}