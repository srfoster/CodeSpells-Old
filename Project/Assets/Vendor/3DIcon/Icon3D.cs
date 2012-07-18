using UnityEngine;
using System.Collections;

public class Icon3D : MonoBehaviour {
	
	public GameObject icon;
	public float y_adj = 3f;
	
	public float x_adj = 0;
	public float z_adj = 0;
	
	int count = 0;

	// Use this for initialization
	void Start () {
		icon = Instantiate(icon, new Vector3(0,0,0), icon.transform.rotation) as GameObject;	
		//icon.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {		
		icon.transform.LookAt(Camera.main.transform.position, Vector3.up);

		
		icon.transform.position = new Vector3(transform.position.x + x_adj,
			transform.position.y+ y_adj + bounce(),
			transform.position.z + z_adj);
	
	}
	
	float bounce()
	{
		return Mathf.Sin(count++) / 5;	
	}
}
