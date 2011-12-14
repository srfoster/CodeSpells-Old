/* See the copyright information at https://github.com/srfoster/Unloader/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class Unloader : MonoBehaviour {
	
	public string text = "Loaded";
	
	
	private Texture2D[] frames;
	private Texture2D loaded_texture;
	private bool loaded = false;
	private GUIStyle label_style;
	
	public void Loaded() {
		loaded = true;	
	}
	
	void Start () {
		frames = new Texture2D[12];
		for(int i = 0; i < 12; i++)
		{
			frames[i] = Resources.Load("Unloader/loader_frame_"+(i+1)) as Texture2D;
		}
		
		loaded_texture = Resources.Load("Unloader/check_mark") as Texture2D;
		
		label_style = new GUIStyle();
		label_style.fontSize = 20;
		label_style.normal.textColor = Color.white;
	}
	
	void OnGUI()
	{
		Texture2D texture;
		
		if(!loaded)
		{
			int index = Mathf.FloorToInt(Time.time * 20) % frames.Length;
			texture = frames[index];	
		} else {
			texture = loaded_texture;
		}
		
		GUI.BeginGroup(new Rect(0,Screen.height - 80,260,80));
		GUI.Box(new Rect(5,5,250,70),"");
		GUI.Label(new Rect(15,30,170,20), text, label_style);
		GUI.DrawTexture(new Rect(195,15,50,50),texture);
		GUI.EndGroup();
	}
}
