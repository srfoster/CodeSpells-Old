using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Icon3D : MonoBehaviour {
	
	public GameObject icon;
	public float y_adj = 3f;
	
	public float x_adj = 0;
	public float z_adj = 0;
	
	int count = 0;
	
	public Color color = Color.green;
	
	Transform mesh;

	void Start () {
		icon = Instantiate(icon, new Vector3(0,0,0), icon.transform.rotation) as GameObject;	
		
		Material my_material = new Material(Shader.Find("Icon/Unlit"));
		
		findMeshRecursive(icon.transform);
		
		mesh.gameObject.renderer.material = my_material;
		mesh.gameObject.renderer.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {	
		if(icon == null)
			return;
		
		icon.transform.LookAt(Camera.main.transform.position, Vector3.up);

		
		icon.transform.position = new Vector3(transform.position.x + x_adj,
			transform.position.y+ y_adj + bounce(),
			transform.position.z + z_adj);
	
	}
	
	void findMeshRecursive(Transform parent)
	{
		if(Regex.Match(parent.name,"Mesh").Success)
		{
			mesh = parent;
			return;
		}
		
		foreach(Transform child in parent)
		{
			findMeshRecursive(child);
		}
	}
	
	float bounce()
	{
		return Mathf.Sin(count++) / 5;	
	}
}
