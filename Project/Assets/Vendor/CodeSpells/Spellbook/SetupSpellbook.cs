using UnityEngine;
using System.Collections;

public class SetupSpellbook : MonoBehaviour {

	public void Init () {
		GameObject prefab = Resources.Load("Spellbook") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;	
	}

}
