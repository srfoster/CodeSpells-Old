using UnityEngine;
using System.Collections;

public class Text3D : MonoBehaviour {
	
	public string text = "";
	public GameObject the_3d_text;

	// Use this for initialization
	void Start () {
		
		if(the_3d_text == null){
			the_3d_text = Instantiate(Resources.Load("Text") as GameObject, transform.position, Quaternion.identity) as GameObject;
			the_3d_text.GetComponent<TextMesh>().alignment = TextAlignment.Center;
			the_3d_text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
		} else {
			the_3d_text = Instantiate(the_3d_text, new Vector3(0,0,0), Quaternion.identity) as GameObject;	
		}
		
		the_3d_text.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!text.Equals(""))
			the_3d_text.GetComponent<TextMesh>().text = text;
		the_3d_text.transform.rotation = GameObject.Find("First Person Controller").transform.rotation;
		the_3d_text.transform.position = new Vector3(transform.position.x,
			transform.position.y+1,
			transform.position.z);
	
	}
}
