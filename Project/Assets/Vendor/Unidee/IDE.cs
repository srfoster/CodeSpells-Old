/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

public class IDE : MonoBehaviour {

	public Texture2D background_texture;
	public Texture2D left_panel_background;
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	public Texture2D control_statement_syntax_highlight;
	public Texture2D expression_syntax_highlight;
	public Texture2D declaration_syntax_highlight;
	public Font ide_font;	
	public GUIStyle button_style = new GUIStyle();
	public GUIStyle code_style = new GUIStyle();
	
	
	private string current_code = "";	
	private IDEInput input;
	private string file_name;	
	private TextEditor stateObj;
	private GameObject previous_state;
	
	private bool paused = false;
	
	public string getCode()
	{
		return current_code;
	}
	
	public string getFileName()
	{
		return file_name;
	}
	
	public int GetCursorPosition()
	{
		return stateObj.selectPos;
	}
	
	public void SetInput(IDEInput input)
	{
		this.input = input;
		current_code = input.GetCode();
		file_name = input.GetFileName();
	}
	
	public void show(GameObject previous_state)
	{
		this.previous_state = previous_state;
		previous_state.active = false;
		enabled = true;
		paused = false;
	}
	
	void Start () {
	
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;
	
		code_style.fontSize = 20;
		code_style.normal.textColor = Color.black;
		code_style.font = ide_font;
		code_style.wordWrap = true;
			
		Thread inputThread  = new Thread (inputProcessing);
		inputThread.Start();
		
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background_texture);
		
		stateObj = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
	
		leftPanel();
		rightPanel();
	}
	
	void leftPanel()
	{
		GUI.BeginGroup(new Rect(0,0,Screen.width*3/4,Screen.height));	
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
		current_code = GUI.TextArea(new Rect(120,40,Screen.width*3/4-200,Screen.height-60),current_code, code_style);
		syntaxHighlight();
		GUI.EndGroup();
	}
	
	void rightPanel()
	{
		GUI.BeginGroup(new Rect(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height));
				
		if (GUI.Button (new Rect (10,15,130,65), "Back", button_style))
		{
	        input.SetCode(current_code);
	
			enabled = false;
	        previous_state.active = true;
			paused = true;
	    }
		
		GUI.EndGroup();
	}
	
	void writeOut()
	{
		input.SetCode(current_code);
	}
	
	/*
	 * The IDE's IDEInput is allowed to directly change the state of the IDE.
	 * Primarily, this is to allow the EclipseInput to communicate with Eclipse
	 * and make appropriate state changes to the Unidee IDE.
	 */ 
	void inputProcessing()
	{
		while(true)
		{
			if(!paused)
			{
				writeOut();
				
				input.Process(this);
				
				Thread.Sleep(500);
			}
		}
	}
	
	void syntaxHighlight()
	{
		string[] lines = current_code.Split("\n"[0]);
	
		int count = 0;
		for(int i = 0; i < lines.Length; i ++)
		{
			string line = lines[i];
			
			highlight("if\\w*(.*)",control_statement_syntax_highlight, line, count);
			highlight("while\\w*(.*)",control_statement_syntax_highlight, line, count);
			highlight("for \\w*(.*)",control_statement_syntax_highlight, line, count);
			//highlight("\\(.*\\)",expression_syntax_highlight, line, count);
			//highlight("object", declaration_syntax_highlight, line, count);
			//highlight("(movement.\\w+\\(.*\\));", declaration_syntax_highlight, line, count);
			
			highlight("public", declaration_syntax_highlight, line, count);
			highlight("private", declaration_syntax_highlight, line, count);
			highlight("static", declaration_syntax_highlight, line, count);
			highlight("void", declaration_syntax_highlight, line, count);
			highlight("class", declaration_syntax_highlight, line, count);
			highlight("import", declaration_syntax_highlight, line, count);

	
			highlight("[0-9]", expression_syntax_highlight, line, count);
			highlight("[0-9.]*f", expression_syntax_highlight, line, count);

			highlight("true", expression_syntax_highlight, line, count);
			highlight("false", expression_syntax_highlight, line, count);

			count++;
		}
		
	}
	
	void highlight(string token, Texture2D texture, string line, int count)
	{
		MatchCollection matches = Regex.Matches(line,token);
	
		for(int i = 0; i < matches.Count; i++)
		{
			Match match = matches[i];
			int offset = match.Groups[0].Index;
			int length = match.Groups[0].Length;
			GUI.DrawTexture(new Rect(118 + offset*12,37+ count * 24, 5 + length * 12, 30), texture);
		}
	}

}
