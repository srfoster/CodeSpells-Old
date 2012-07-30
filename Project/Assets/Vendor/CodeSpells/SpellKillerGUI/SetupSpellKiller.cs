using UnityEngine;
using System.Collections;

public class SetupSpellKiller : MonoBehaviour {

	void Start () {
		Init();
	}
	
	public void Init(){
		GameObject prefab = Resources.Load("SpellKiller") as GameObject;
		
		GameObject inventory = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		inventory.name = prefab.name;	
	}
}
