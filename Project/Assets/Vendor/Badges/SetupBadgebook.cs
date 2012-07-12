using UnityEngine;
using System.Collections;

public class SetupBadgebook : MonoBehaviour {
	

	public void Init () {
		GameObject prefab = Resources.Load("Badgebook") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;	
	}

}
