using UnityEngine;
using System.Collections;

public class SetupUnidee : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	public void Init(){
		GameObject prefab = Resources.Load("IDE") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;		}
	
}
