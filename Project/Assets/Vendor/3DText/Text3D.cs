using UnityEngine;
using System.Collections;

public class Text3D : MonoBehaviour {
	
	public string text = "";
	GameObject the_3d_text;

	// Use this for initialization
	void Start () {
		the_3d_text = Instantiate(Resources.Load("Text") as GameObject, transform.position, Quaternion.identity) as GameObject;
		the_3d_text.GetComponent<TextMesh>().alignment = TextAlignment.Center;
		the_3d_text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
	}
	
	// Update is called once per frame
	void Update () {
		the_3d_text.GetComponent<TextMesh>().text = text;
		the_3d_text.transform.rotation = GameObject.Find("First Person Controller").transform.rotation;
		the_3d_text.transform.position = new Vector3(transform.position.x,
			transform.position.y+1,
			transform.position.z);

	}
}
