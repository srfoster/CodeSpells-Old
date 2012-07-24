using UnityEngine;
using System.Collections;

public class CollectBread : MonoBehaviour {
	private int collectedCount = 0;
	public delegate void EventHandler();
	public static event EventHandler CollectedBread;
	
	void onTriggerEnter(Collider col)
	{
		if(!col.gameObject.name.EndsWith("QuestBread"))
			return;
		
		Destroy(col.gameObject);
		
		collectedCount++;
		
		if(collectedCount == 12)
		{
			if(CollectedBread != null)
				CollectedBread();	
		}
	}
}