using UnityEngine;
using System.Collections;

public class SeeIt : MonoBehaviour {
	
	public bool should_show = false;
	public bool should_destroy = false;
	
	void OnDrawGizmos () {
		
		if(should_show && GameObject.FindWithTag("Player") == null)
		{
			should_show = false;
			(new SetupCodeSpells()).Init();
		}
		
		
		if(should_destroy){
			should_destroy = false;
			DestroyImmediate(GameObject.Find("ConversationDisplayer"));	
			DestroyImmediate(GameObject.Find("Forest"));	
			DestroyImmediate(GameObject.Find("IDE"));
			DestroyImmediate(GameObject.Find("Inventory"));
			DestroyImmediate(GameObject.Find("Popup"));
			DestroyImmediate(GameObject.Find("Server"));
			DestroyImmediate(GameObject.Find("InitialScroll"));
			DestroyImmediate(GameObject.Find("Camp"));
			DestroyImmediate(GameObject.Find("temp"));

			Fancy.Clear();
		}
	}
	

}
