using UnityEngine;
using System.Collections;

public class SetupHighlighter : MonoBehaviour {

	// Use this for initialization
	public void Init () {
		GameObject prefab = Resources.Load("Highlighter") as GameObject;
		
		GameObject obj = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		obj.name = prefab.name;	
	}
}
