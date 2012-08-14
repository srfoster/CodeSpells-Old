using UnityEngine;
using System.Collections;

public class ExampleDisplayHealth : MonoBehaviour {
	
	public Texture2D red_heart;
	public Texture2D blue_heart;
	private Health health;
	
	// Use this for initialization
	void Start () {
		health = gameObject.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		Debug.Log ("current health is: "+health.myHealth);
		float heart_dim = 20f;
		
		drawHearts(red_heart, 15, Screen.height-50, 10);
		drawHearts (blue_heart, 15, Screen.height-30, 10);
		
	}
	
	void drawHearts(Texture2D type,float x,float y,int numHearts) {
		
		float heart_dim = 20f;
		
		int i = 0;
		while( i!=numHearts ){
			GUI.DrawTexture (new Rect(x+heart_dim*i, y, heart_dim,heart_dim), type);
			i++;
		}
	}
}
