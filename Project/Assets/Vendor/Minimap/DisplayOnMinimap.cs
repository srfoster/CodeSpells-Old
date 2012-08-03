using UnityEngine;
using System.Collections;

public class DisplayOnMinimap : MonoBehaviour {
	
	private GameObject triangle;
	
	public Color color = Color.green;

    public float scale = 0.01f;
	
	void Start () {
		GameObject trianglePrefab = (Resources.Load("Triangle") as GameObject);
		triangle = (Instantiate(trianglePrefab, transform.position, Quaternion.identity) as GameObject);
		triangle.name = "Triangle";

		
		Material my_material = new Material(Shader.Find("Diffuse"));
		
		triangle.transform.FindChild("Mesh").gameObject.renderer.material = my_material;
		triangle.transform.FindChild("Mesh").gameObject.renderer.material.color = color;
		
		triangle.transform.localScale *= scale;
	}
	
	void Update () {
		Vector3 adjPos = new Vector3(transform.position.x, 60,transform.position.z);
		triangle.transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
		triangle.transform.position = adjPos;


    }
}
