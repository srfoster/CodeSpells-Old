using UnityEngine;
using System.Collections;

public class SetupPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	public void Init()
	{
		GameObject prefab = Resources.Load("Popup") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;		}
	
}
