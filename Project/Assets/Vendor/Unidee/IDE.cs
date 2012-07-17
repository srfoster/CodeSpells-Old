/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
	public Texture2D error_syntax_highlight;

	
	public Font ide_font;	
	public GUIStyle button_style = new GUIStyle();
	public GUIStyle code_style = new GUIStyle();
	
	
	private string current_code = "";	
	private IDEInput input;
	private string file_name;	
	private TextEditor stateObj;
	private GameObject previous_state;
	
	private bool thread_started = false;
	
	private double unpause_count = 0;
	
	private bool no_edit = false;
	
	private bool paused = false;
	
	private Vector2 scroll_position = Vector2.zero;
	
	private bool ignore_scroll = false;
	
	private int last_line_number = 0;
	
	private TestAutoCompletionManager auto_completion_manager = new TestAutoCompletionManager();
	
	private string current_error;
	
	private List<LineStyle> line_styles = new List<LineStyle>();
	
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
	
	public int LineNumber()
	{
		return NumberOfNewLines(current_code.Substring(0,GetCursorPosition()));	//Number of newlines before cursor
	}
	
	public int CursorVerticalOffset()
	{
		return LineNumber() * 24;	
	}
	
	public int NewLineBeforeCursor()
	{
		for(int i = GetCursorPosition()-1; i >=0; i--)
		{
			char c = current_code[i];
			if (c == '\n')
				return i;
		}
		return -1;
	}
	
	public int ColumnNumber()
	{
		return (GetCursorPosition() - NewLineBeforeCursor()) - 1;	
	}
	
	public int CursorHorizontalOffset()
	{
		return ColumnNumber() * 12;
	}
	
	public int NumberOfNewLines(string s)
	{
		int count = 0;
		foreach(char i in s)
		{
			if (i == '\n')
				count++;
		}
		return count;
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
		no_edit = false;
		
		Time.timeScale = 0;
	}
	
	void Start () {
		Init();
			

		
	}
	
	public void Init()
	{
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;
	
		code_style.fontSize = 20;
		code_style.normal.textColor = Color.black;
		code_style.font = ide_font;
		code_style.wordWrap = false;	
	}
	
	void OnGUI()
	{

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background_texture);
		
		stateObj = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
	
		leftPanel();
		rightPanel();

		adjustScroll();
		
		catchTabs();
		
		detectKeyboardActivity();
		
	}
	
	void detectKeyboardActivity()
	{
		unpause_count += .1;
		
//		Debug.Log(unpause_count);
		
		if(Event.current.isKey)
		{
			unpause_count = 0;
			thread_started = false;
		}
		

		
		if(unpause_count > 2 && !thread_started)  // Unpause stuff like compilation after every 5 seconds of inactivity
		{
			Debug.Log("Starting thread");
			thread_started = true;
			Thread inputThread  = new Thread (inputProcessing);
			inputThread.Start();
		}
	}
	
	
	void suggestAutocompletes()
	{
		int x = CursorHorizontalOffset();
		int y = CursorVerticalOffset() + 24;
		
		Suggestions suggestions = auto_completion_manager.getSuggestions();
		
		GUIStyle suggestion_style = new GUIStyle();
		
		suggestion_style.fontSize = 20;
		suggestion_style.normal.textColor = Color.black;
		suggestion_style.font = ide_font;
		suggestion_style.wordWrap = false;
		suggestion_style.normal.background = Resources.Load("Textures/WoodenButtonUp") as Texture2D;
		suggestion_style.alignment = TextAnchor.MiddleCenter;
		
		//GUI.Box(new Rect(x,y, suggestions.longest().Length * 12 + 30, suggestions.number() * 24 + 30), suggestions.toString(), suggestion_style);
	}
		
		
	void catchTabs()
	{
		if( Event.current.Equals( Event.KeyboardEvent("tab") ) )
	    {
	        Event.current.Use();
			
			current_code = current_code.Substring(0,GetCursorPosition()) + "    " + current_code.Substring(GetCursorPosition());
			stateObj.MoveRight();
		}
	}
	
	void adjustScroll()
	{
		if(last_line_number == LineNumber())
		{
			return;
		}
		
		last_line_number = LineNumber();
		
		if(CursorVerticalOffset() > scroll_position.y + Screen.height - 60)
		{
			scroll_position.y += CursorVerticalOffset() - (scroll_position.y + Screen.height - 60);	
		}
		
		if(CursorVerticalOffset() < scroll_position.y)
		{
			scroll_position.y += CursorVerticalOffset() - scroll_position.y;
		}
	}
	
	void leftPanel()
	{


		GUI.BeginGroup(new Rect(0,0,Screen.width*3/4,Screen.height));
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
		
		GUI.BeginGroup(new Rect(120,40,Screen.width*3/4,Screen.height));
		
		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width*3/4-200), GUILayout.Height(Screen.height-60 )); // Should vary the size of the last rect by how much text we have??
		showCode();
		GUILayout.EndScrollView ();	

		
		syntaxHighlight();

	//	suggestAutocompletes();

		
		GUI.EndGroup();

		
		GUI.EndGroup();
	}
	
	public void showCode(){
		if(no_edit)
			GUILayout.TextArea(current_code, code_style);
		else
    		current_code = GUILayout.TextArea(current_code, code_style);
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
			Time.timeScale = 1;
	    }
		
		
		GUIStyle style = GUI.skin.box;
		
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		
		GUI.Box(new Rect (10,100,230,500), current_error, style);
		
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
		writeOut();
		input.Process(this);			
	}
	
	public void noEdit(){
		no_edit = true;	
	}
	
	public void syntaxHighlight()
	{
		string[] lines = current_code.Split("\n"[0]);
	
		int count = 0;
		for(int i = 0; i < lines.Length; i ++)
		{
			string line = lines[i];
			
			bool do_syntax_highlight = true;
			
			foreach(LineStyle style in line_styles)
			{
				if(style.number == i)
				{
					highlight(".*", style.texture, line, count);
					
					do_syntax_highlight = false;
				}
			}
			
			
			if(do_syntax_highlight){
				highlight("if\\w*(.*)",control_statement_syntax_highlight, line, count);
				highlight("while\\w*(.*)",control_statement_syntax_highlight, line, count);
				highlight("for \\w*(.*)",control_statement_syntax_highlight, line, count);
							
				highlight("Enchanted", declaration_syntax_highlight, line, count);
				highlight("EnchantedList", declaration_syntax_highlight, line, count);
				highlight("String", declaration_syntax_highlight, line, count);
				highlight("Location", declaration_syntax_highlight, line, count);
				highlight("getTarget", declaration_syntax_highlight, line, count);
				highlight("getByName", declaration_syntax_highlight, line, count);
				highlight("getLocation", declaration_syntax_highlight, line, count);
	
	
				highlight("true", expression_syntax_highlight, line, count);
				highlight("false", expression_syntax_highlight, line, count);
			}

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
			GUI.DrawTexture(new Rect(-2 + offset*12 - scroll_position.x,-2 + count * 24 - scroll_position.y, 5 + length * 12, 30), texture);
		}
	}
	
	public void setErrorMessage(string error)
	{
		current_error = error;
	}
	
	public void addStyle(int number, string type){
		LineStyle style = new LineStyle();
		style.number = number;
		
		if(type.Equals("error"))
		{
			style.texture = error_syntax_highlight;	
		}
		
		line_styles.Add(style);
	}
	
	public void clearStyles()
	{
		line_styles.Clear();	
	}
	
	public class LineStyle{
		public int number = -1;
		public Texture2D texture;
	}
}
