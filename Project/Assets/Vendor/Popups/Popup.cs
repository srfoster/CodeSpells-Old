using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {
	
	private GUIStyle style;
	
	private float time = 0f;
	private string text;
	
	void Start()
	{
		  style = new GUIStyle();
		  style.normal.background = Resources.Load("Textures/SyntaxHighlightBlue") as Texture2D;
		  style.normal.textColor = Color.white;
		  style.alignment = TextAnchor.MiddleCenter;
		  style.fontSize = 17;
	}

	void OnGUI()
	{
		if(time < 0)
			return;
		
		if(time < 1)
			GUI.color = new Color(1f,1f,1f,time);
		
		GUI.Box(new Rect(Screen.width/2 - 250,10,500,100),text,style);
		
		time -= Time.deltaTime;
	}
	
	public void popup(string text)
	{
		Debug.Log("Popping up :" + text);
		this.time = 5.0f;
		this.text = text;
	}
}
