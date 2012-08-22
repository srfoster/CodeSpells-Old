/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Threading;
using System.IO;




public class IDE : MonoBehaviour {
	
	public delegate void EventHandler(string file_name, string contents);
	public static event EventHandler IDEOpened;
	public static event EventHandler IDEClosed;

	public Texture2D background_texture;
	public Texture2D left_panel_background;
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	public Texture2D red_button_up_texture;
	public Texture2D red_button_down_texture;
	
	public Font ide_font;	
	public GUIStyle button_style = new GUIStyle();
	public GUIStyle remove_style = new GUIStyle();
	public GUIStyle code_style = new GUIStyle();
	
	
	private string current_code = "";
	private IDEInput input;
	private string file_name;	
	private TextEditor stateObj;
	private GameObject previous_state;
	
	private bool shouldPopup;
	private bool shouldRemove;
	private Rect popupWindow;
	private int box_width = 300;
	private int box_height = 40;
	
	private Thread inputThread;
	
	private double unpause_count = 0;
	
	private bool no_edit = false;
	
	private bool paused = false;
	
	private Vector2 scroll_position = Vector2.zero;
	
	private bool ignore_scroll = false;
	
	private int last_line_number = 0;
	
	private TestAutoCompletionManager auto_completion_manager = new TestAutoCompletionManager();
	
	private string current_error;
	
	private List<int> error_lines = new List<int>();
	
	//private List<LineStyle> line_styles = new List<LineStyle>();
	
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
		input.Process (this);
		this.input = input;
		current_code = input.GetCode();
		file_name = input.GetFileName();
		scroll_position = Vector2.zero;
	}
	
	public void show(GameObject previous_state)
	{
		this.previous_state = previous_state;
		previous_state.active = false;
		enabled = true;
		paused = false;
		
		//Time.timeScale = 0;
		
		if(IDEOpened != null)
			IDEOpened(file_name, current_code);
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
		
		remove_style.normal.background = red_button_up_texture;
		remove_style.active.background = red_button_down_texture;
		remove_style.normal.textColor = Color.black;
		remove_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		remove_style.alignment = TextAnchor.MiddleCenter;
		remove_style.fontSize = 30;
	
		code_style.fontSize = 20;
		code_style.normal.textColor = Color.black;
		code_style.font = ide_font;
		code_style.wordWrap = false;
		
		shouldPopup = shouldRemove = false;
	}
	
	void DoWindow0 (int windowID) {
		if (GUI.Button(new Rect(10, 20, 20, 20), "OK")) {
			shouldPopup = false;
		}
		
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background_texture);
		
		stateObj = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
		
		
		if (shouldPopup) {
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
		GUILayout.BeginArea(new Rect(120,40,Screen.width*3/4,Screen.height));
		
		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width*3/4-200), GUILayout.Height(Screen.height-60 )); // Should vary the size of the last rect by how much text we have??
			
		GUILayout.EndScrollView ();	
		GUILayout.EndArea();
			
			int boxWidth = 300;
			int boxHeight = 75;
			GUIStyle style = GUI.skin.box;
			style.alignment = TextAnchor.UpperCenter;
			GUI.Box(new Rect (Screen.width/2-boxWidth/2, Screen.height/2-boxHeight/2, boxWidth, boxHeight), "Are you sure you want to delete your script?", style);
			
			if (GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+box_height/2-20, 50, 30), "Yes")) {
				no_edit = false;
				shouldRemove = true;
				shouldPopup = false;
			}
			
			if (GUI.Button(new Rect(Screen.width/2+30, Screen.height/2+box_height/2-20, 50, 30), "No")) {
				no_edit = false;
				shouldPopup = false;
			}
		}
		else {
			leftPanel();
		}
		
		if (shouldRemove)
			removeScript();
		
		rightPanel();
		
		adjustScroll();
		
		catchTabs();
		
		detectKeyboardActivity();
		
	}
	
	void removeScript()
	{
		shouldRemove = false;
		current_code = "";
		input.SetCode(current_code);	
		
		enabled = false;
	    previous_state.active = true;
		paused = true;
		Time.timeScale = 1;
		
		if(IDEClosed != null) {
			
			IDEClosed(file_name, current_code);
			
		}
	}
		
	
	void detectKeyboardActivity()
	{
		unpause_count += .1;

		if(Event.current.isKey)
		{	
			unpause_count = 0;
		}
		
		if(unpause_count > 2 && (inputThread == null || !inputThread.IsAlive))  // Unpause stuff like compilation after every 5 seconds of inactivity
		{
			Debug.Log("Starting thread");
			inputThread = new Thread (inputProcessing);
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
		
		//Insert tab if they press tab.
		if( Event.current.Equals( Event.KeyboardEvent("tab") ) )
	    {
	        Event.current.Use();
			
			insertTab(2);
		}
		
		//If they press Enter, figure out the new indentation, and do an auto indent
		
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return)
		{
			
			
			string up_to_cursor = current_code.Substring(0, GetCursorPosition());
			
			int num_indents = 0;
			for(int i = 0; i < up_to_cursor.Length; i++)
			{
				char current = up_to_cursor[i];
				
				if(current == '{')
					num_indents++;
				
				if(current == '}')
					num_indents--;
			}

						
			for(int i = 0; i < num_indents; i++)
			{
				insertTab(2);	
			}
		}
		
	}
	
	void insertTab(int tab_width)
	{
		string spaces = "";
		
		for(int i = 0; i < tab_width; i++)
			spaces += " ";
		
		current_code = current_code.Substring(0,GetCursorPosition()) + spaces + current_code.Substring(GetCursorPosition());
		
		for(int i = 0; i < tab_width; i++)
			stateObj.MoveRight();
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
		GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
		GUILayout.BeginArea(new Rect(120,40,Screen.width*3/4,Screen.height));
		
		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width*3/4-200), GUILayout.Height(Screen.height-60 )); // Should vary the size of the last rect by how much text we have??
		foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightPage(current_code)) {
			GUI.DrawTexture(tup.Item1, tup.Item2);
		}
		foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightErrors(current_code, error_lines)) {
			GUI.DrawTexture(tup.Item1, tup.Item2);
		}
		showCode();
		GUILayout.EndScrollView ();	
		GUILayout.EndArea();
	}
	
	public void showCode(){

			if(no_edit) {
				GUILayout.TextArea(current_code, code_style);
			}
			else
    			current_code = GUILayout.TextArea(current_code, code_style);
	}
	
	void rightPanel()
	{
		GUI.BeginGroup(new Rect(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height));
		if (GUI.Button (new Rect (10,15,130,65), "Back", button_style))
		{
			if (!shouldPopup) {
				//back button will change the SpellBook page name
	        	input.SetCode(current_code);
				
				enabled = false;
	        	previous_state.active = true;
				paused = true;
				Time.timeScale = 1;
					
				if(IDEClosed != null)
					IDEClosed(file_name, current_code);
			}
	    }
		
		
		if (GUI.Button (new Rect (180,15,65,65), "X", remove_style))
		{
			no_edit = true;
			shouldPopup = true;
	    }
		
		GUIStyle style = GUI.skin.box;
		
		style.alignment = TextAnchor.UpperLeft;
		style.wordWrap = true;
		
		string pattern = @"^(.+) class (.+) is public, should be declared in a file named (.+)";
		if (!Regex.IsMatch(current_error, pattern))
			GUI.Box(new Rect (10,100,230,500), current_error, style);
		else {
			GUI.Box(new Rect (10,100,230,500), "------------", style);
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
		writeOut();
		input.Process(this);			
	}
	
	public void setErrorMessage(string error)
	{
		current_error = error;
	}
	
	public void addStyle(int number, string type){
		string pattern = @"^(.+) class (.+) is public, should be declared in a file named (.+)";
		if (Regex.IsMatch(current_error, pattern))
			return;
		
		
		
		if(type.Equals("error"));
		{
			error_lines.Add(number);	
		}

	}
	
	public void clearStyles()
	{
		error_lines.Clear();
	}
	
}
