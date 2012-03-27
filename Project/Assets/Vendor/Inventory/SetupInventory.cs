using UnityEngine;
using System.Collections;

public class SetupInventory : MonoBehaviour {

	void Start () {
		Init();
	}
	
	public void Init(){
		GameObject prefab = Resources.Load("Inventory") as GameObject;
		
		GameObject inventory = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		inventory.name = prefab.name;	
	}

}
