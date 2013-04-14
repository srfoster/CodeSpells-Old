using UnityEngine;
using System.Collections;

public class SetupJune : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	public void Init()
	{
		GameObject oldServer = GameObject.Find("Server");
		
		GameObject prefab = Resources.Load("Server") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;		}
}
