using UnityEngine;
using System.Collections;

public class DisplayOnMinimap : MonoBehaviour {
	
	private GameObject dot;
	
	void Start () {
		GameObject dotPrefab = (Resources.Load("Dot") as GameObject);
		dot = (Instantiate(dotPrefab, transform.position, Quaternion.identity) as GameObject);
	
	}
	
	void Update () {
		Vector3 adjPos = new Vector3(transform.position.x, 60,transform.position.z);
		dot.transform.position = adjPos;
	
	}
}
