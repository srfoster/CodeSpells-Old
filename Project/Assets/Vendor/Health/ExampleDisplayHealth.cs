using UnityEngine;
using System.Collections;

public class ExampleDisplayHealth : MonoBehaviour {
	
	public Texture2D heart_texture;
	public float maxNumHearts = 10f;
	public float x_offset = 15f;
	public float heart_dim = 20f;
	public float heightFromBottom = 50f;
	
	private Health health;
	
	// Use this for initialization
	void Start () {
		
		
		health = gameObject.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		float currentHealth = (float) health.myHealth;
		
		//Debug.Log (currentHealth);
		
		int numHearts = Mathf.CeilToInt(currentHealth / maxNumHearts);
		
		drawHearts(heart_texture, x_offset, Screen.height-heightFromBottom, numHearts);
		
	}
	
	void drawHearts(Texture2D heart_texture, float x,float y,int numHearts) {
		
		int i = 0;
		while( i!=numHearts ){
			GUI.DrawTexture (new Rect(x+heart_dim*i, y, heart_dim,heart_dim), heart_texture);
			i++;
		}
	}
}
