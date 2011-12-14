/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class IDEButton2 : MonoBehaviour {
	
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	
	private GUIStyle button_style = new GUIStyle();
	
	private FileInput input;
	
	void Start(){
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;	
		
		input = new EclipseInput("UnideeJavaCode",Application.dataPath + "/Vendor/Unidee/Examples/UnideeJavaCode/Example.java");
	}

	// Use this for initialization
	void OnGUI(){
		if (GUI.Button (new Rect (10,100,130,65), "File", button_style))
		{
	        IDE ide = (GameObject.Find("IDE").GetComponent("IDE") as IDE);
			ide.SetInput(input);
			ide.show(gameObject);
	    }	
	}
}
