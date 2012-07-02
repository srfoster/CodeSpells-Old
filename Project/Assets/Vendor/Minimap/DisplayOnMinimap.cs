using UnityEngine;
using System.Collections;

public class DisplayOnMinimap : MonoBehaviour {
	
	private GameObject triangle;
	
	void Start () {
		GameObject trianglePrefab = (Resources.Load("Triangle") as GameObject);
		triangle = (Instantiate(trianglePrefab, transform.position, Quaternion.identity) as GameObject);
	
	}
	
	void Update () {
		Vector3 adjPos = new Vector3(transform.position.x, 60,transform.position.z);
		triangle.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
		triangle.transform.position = adjPos;
	}
}
