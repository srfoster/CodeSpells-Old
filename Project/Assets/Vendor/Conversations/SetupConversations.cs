using UnityEngine;
using System.Collections;

public class SetupConversations : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	public void Init()
	{
		GameObject prefab = Resources.Load("ConversationDisplayer") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;		}
	
}
