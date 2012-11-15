using UnityEngine;
using System.Collections;

public class SetupPracticeRoomSpellbook : MonoBehaviour {
	bool enabled = false;

	public void Init () {
		GameObject prefab = Resources.Load("PracticeRoomSpellbook") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;	
	}


}
