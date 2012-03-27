using UnityEngine;
using System.Collections;

public class SetupBasicWorld : MonoBehaviour {
	
	public SetupBasicWorld()
	{
		
	}
	
	public void Init(){
		GameObject prefab = Resources.Load("Forest") as GameObject;
		
		GameObject obj = Fancy.InstantiatePrefab(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;	
	}
	
}
