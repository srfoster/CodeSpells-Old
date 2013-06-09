/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */

// To use old IDE, comment out this line
#define NEWIDE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System;
using System.Reflection;




#if (NEWIDE)

struct Rectangle {
    public float x, y, w, h;
    
    public Rectangle(float xcoord, float ycoord, float width, float height) {
        x = xcoord;
        y = ycoord;
        w = width;
        h = height;
    }
    
    public Rect getRect() {
        return new Rect(x,y,w,h);
    }
}





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
	private GUIStyle empty_style = new GUIStyle();
	
	//private Rectangle editorPanel;
	private EditorPanel editorPanel;
	private ErrorPanel errorPanel;
	//private Rectangle errorPanel;
	private ButtonPanel buttonPanel;
	//private Rectangle referencePanel;
	private Spellbook spellbook;
	private HalfBook halfbook;
	
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
	
	private bool changed = false;
	
	private bool no_edit = false;
	
	private bool paused = false;
	
	private Vector2 scroll_position = Vector2.zero;
	
	private bool ignore_scroll = false;
	
	private int last_line_number = 0;
	
	private TestAutoCompletionManager auto_completion_manager = new TestAutoCompletionManager();
	
	private string current_error;
	private string last_error = "";
	
	private string selected = "";
	//private string clipboard = (string) typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null,null);
	private string clipboard = "";
	private int selStart = 0;
	private int selStop = 0;
	
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
	
	public string getSpellName()
	{
	    char[] seps = {'.'};
	    string s = file_name.Substring(file_name.LastIndexOf("/")+1);
	    return s.Split(seps)[0];
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
	
	public string getCurrentError() {
	    return current_error;
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
		
		ProgramLogger.LogKV("open", getSpellName()+", "+Time.time);
		
		spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
		halfbook = new HalfBook();
		
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
		
		//editorPanel = new Rectangle(0,0,Screen.width*3/4,Screen.height);
		editorPanel = new EditorPanel(this);
		editorPanel.setTexture(left_panel_background);
		//editorPanel.scale((float) 0.5, (float) 0.75);
		//editorPanel.move(800, 150);
		errorPanel = new ErrorPanel(this);
		errorPanel.setTexture(left_panel_background);
		//errorPanel.scale((float) 2, (float) 0.25);
		//errorPanel.move(-200, 500);
		//errorPanel = new Rectangle(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height);
		buttonPanel = new ButtonPanel(this);
	}
	
	void DoWindow0 (int windowID) {
		if (GUI.Button(new Rect(10, 20, 20, 20), "OK")) {
			shouldPopup = false;
		}
		
	}
	
	void OnGUI()
	{
	    
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background_texture);
		
		// this must come before the TextEditor line below
		halfbook.draw();
		
		GUI.SetNextControlName("EditableCode");
		stateObj = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
		//GUI.FocusControl("EditableCode");
		
		// Give the appropriate text area keyboard control -> this prevents us from moving the player while in the IDE
// 		if (Event.current.rawType == EventType.MouseDown) {
// 		    Debug.Log("caught mousedown event");
// 		    if (editorPanel.isWithin(Event.current.mousePosition))
// 		        GUI.FocusControl("EditableCode");
// 		    else
// 		        GUI.FocusControl("ReferenceCode");
// 		    Debug.Log("focus control: "+GUI.GetNameOfFocusedControl());
// 		}
		
		
		
		if (shouldPopup) {
// 		GUI.DrawTexture(editorPanel.getRect(),left_panel_background);
// 		//GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
// 		GUILayout.BeginArea(new Rect(120,40,Screen.width*3/4,Screen.height));
// 		
// 		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width*3/4-200), GUILayout.Height(Screen.height-60 )); // Should vary the size of the last rect by how much text we have??
// 			
// 		GUILayout.EndScrollView ();	
// 		GUILayout.EndArea();
            
            editorPanel.draw(false);
			
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
			//leftPanel();
			editorPanel.draw(true);
		}
		
		if (shouldRemove)
			removeScript();
		
		//rightPanel();
		errorPanel.draw();
		buttonPanel.draw();
		
		if (GUI.GetNameOfFocusedControl() == "EditableCode") {
            adjustScroll();
        
            catchTabs();
        
            detectKeyboardActivity();
                
            logKeyboardActivity();
		}
		
		// check if the spellbook (outside of the code region) was clicked
		//      if so, we want to make it full screen
		if (Event.current.type == EventType.MouseDown) {
		    Vector2 pos = Event.current.mousePosition;
		    if (halfbook.getBookRect().Contains(pos) && !halfbook.getTextRect().Contains(pos) && !errorPanel.getErrorRect().Contains(pos) && !editorPanel.getEditorRect().Contains(pos)) {
		        spellbook.setNoCopyDisplay(true);
		        spellbook.show(GameObject.Find("IDE"));
		    }
		}
		
		// make it so that we can't click through to the game
        // NOTE: This must appear LAST in the OnGUI. Otherwise, other buttons won't work!
        GUI.Button(new Rect(0,0,Screen.width,Screen.height), "", empty_style);
		
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
		
		ProgramLogger.LogKVtime("delete", "all");
		ProgramLogger.LogKV("close", getSpellName()+", "+Time.time);
		ProgramLogger.LogCode(getSpellName(), current_code);
				
		if(IDEClosed != null) {
			
			IDEClosed(file_name, current_code);
			
		}
	}
		
	
	void detectKeyboardActivity()
	{
		if (GUI.changed || changed)
		{
		    if (inputThread == null || !inputThread.IsAlive) {
		        changed = false;
                Debug.Log("Starting thread");
                inputThread = new Thread (inputProcessing);
                inputThread.Start();
			} else
			    changed = true;
		}
	}
	
	
	void logKeyboardActivity()
	{
	    if (GUI.changed && GUI.GetNameOfFocusedControl() == "EditableCode") {
		    clipboard = (string) typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null,null);
		    
		    Event e = Event.current;
		    if ( ((int)e.character) != 0 && String.Equals("None", e.keyCode+"") ) {  //type a character
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("insert", stateObj.pos-1, stateObj.pos, e.character+"");
		        else {
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		            ProgramLogger.LogEdit("insert", stateObj.pos-1, stateObj.pos, e.character+"");
		        }
		    } else if ((e.functionKey && String.Equals(e.keyCode+"", "Backspace")) || (String.Equals(""+e.keyCode, "X") && (e.command || e.control))) {   //delete or cut
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("remove", stateObj.pos, selStart, "");
		        else
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		    } else if ((e.command || e.control) && String.Equals(""+e.keyCode, "V")) {    //paste
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("insert", stateObj.pos-clipboard.Length, stateObj.pos, clipboard);
		        else {
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		            ProgramLogger.LogEdit("insert", stateObj.pos-clipboard.Length, stateObj.pos, clipboard);
		        }
		    }
		}
		
		selected = stateObj.SelectedText+"";
		selStart = Math.Min(stateObj.pos, stateObj.selectPos);
		selStop = Math.Max(stateObj.pos, stateObj.selectPos);

	}
	
	
	void logNewErrors()
	{
	    if (! String.Equals(current_error, last_error)) {
            char[] trimchars = {'-', '\n', ' '};
            string cleanerror = current_error.Trim(trimchars);
            if (!String.IsNullOrEmpty(cleanerror))
                ProgramLogger.LogError(cleanerror);
            last_error = current_error;
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
		ProgramLogger.LogEdit("insert", GetCursorPosition(), GetCursorPosition()+tab_width, spaces);
		
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
	
// 	void leftPanel()
// 	{
// 	    GUI.DrawTexture(editorPanel.getRect(),left_panel_background);
// 		//GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
// 		GUILayout.BeginArea(new Rect(120,40,Screen.width*3/4,Screen.height));
// 		
// 		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width*3/4-200), GUILayout.Height(Screen.height-60 )); // Should vary the size of the last rect by how much text we have??
// 		foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightPage(current_code)) {
// 			GUI.DrawTexture(tup.Item1, tup.Item2);
// 		}
// 		foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightErrors(current_code, error_lines)) {
// 			GUI.DrawTexture(tup.Item1, tup.Item2);
// 		}
// 		showCode();
// 		GUILayout.EndScrollView ();	
// 		GUILayout.EndArea();
// 	}
    
    public void showHighlightedCode() {
        GUILayout.BeginArea(editorPanel.getTextAreaRect());
        scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(editorPanel.getScrollWidth()), GUILayout.Height(editorPanel.getScrollHeight())); // Should vary the size of the last rect by how much text we have??
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
	
// 	void rightPanel()
// 	{
// 	    //GUI.BeginGroup(errorPanel.getRect());
// 		GUI.BeginGroup(new Rect(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height));
// 		if (GUI.Button (new Rect (10,15,130,65), "Back", button_style))
// 		{
// 			if (!shouldPopup) {
// 				//back button will change the SpellBook page name
// 	        	input.SetCode(current_code);
// 				
// 				enabled = false;
// 	        	previous_state.active = true;
// 				paused = true;
// 				Time.timeScale = 1;
// 				
// 				ProgramLogger.LogKV("close", getSpellName()+", "+Time.time);
// 				ProgramLogger.LogCode(getSpellName(), current_code);
// 				 					
// 				if(IDEClosed != null)
// 					IDEClosed(file_name, current_code);
// 			}
// 	    }
// 		
// 		
// 		if (GUI.Button (new Rect (180,15,65,65), "X", remove_style))
// 		{
// 			no_edit = true;
// 			shouldPopup = true;
// 	    }
// 		
// // 		GUIStyle style = GUI.skin.box;
// // 		
// // 		style.alignment = TextAnchor.UpperLeft;
// // 		style.wordWrap = true;
// // 		
// // 		string pattern = @"^(.+) class (.+) is public, should be declared in a file named (.+)";
// // 		if (!Regex.IsMatch(current_error, pattern))
// // 			GUI.Box(new Rect (10,100,230,500), current_error, style);
// // 		else {
// // 			GUI.Box(new Rect (10,100,230,500), "------------", style);
// // 		}
// 		
// 		GUI.EndGroup();
// 	}
	
	public void checkBackButton(Rect r) {
	    if (GUI.Button (r, "Back", button_style))
		{
			if (!shouldPopup) {
				//back button will change the SpellBook page name
	        	input.SetCode(current_code);
				
				enabled = false;
	        	previous_state.active = true;
				paused = true;
				Time.timeScale = 1;
				
				ProgramLogger.LogKV("close", getSpellName()+", "+Time.time);
				ProgramLogger.LogCode(getSpellName(), current_code);
									
				if(IDEClosed != null)
					IDEClosed(file_name, current_code);
			}
	    }
	}
	
	public void checkRemoveButton(Rect r) {
	    if (GUI.Button (r, "X", remove_style))
		{
			no_edit = true;
			shouldPopup = true;
	    }
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
		logNewErrors();
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
























// Old IDE
#else


struct Rectangle {
    public float x, y, w, h;
    
    public Rectangle(float xcoord, float ycoord, float width, float height) {
        x = xcoord;
        y = ycoord;
        w = width;
        h = height;
    }
    
    public Rect getRect() {
        return new Rect(x,y,w,h);
    }
}


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
	
	private Rectangle editorPanel;
	private Rectangle errorPanel;
	private Rectangle referencePanel;
	
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
	
	private bool changed = false;
	
	private bool no_edit = false;
	
	private bool paused = false;
	
	private Vector2 scroll_position = Vector2.zero;
	
	private bool ignore_scroll = false;
	
	private int last_line_number = 0;
	
	private TestAutoCompletionManager auto_completion_manager = new TestAutoCompletionManager();
	
	private string current_error;
	private string last_error = "";
	
	private string selected = "";
	//private string clipboard = (string) typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null,null);
	private string clipboard = "";
	private int selStart = 0;
	private int selStop = 0;
	
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
	
	public string getSpellName()
	{
	    char[] seps = {'.'};
	    string s = file_name.Substring(file_name.LastIndexOf("/")+1);
	    return s.Split(seps)[0];
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
		
		ProgramLogger.LogKV("open", getSpellName()+", "+Time.time);
		
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
		
		editorPanel = new Rectangle(0,0,Screen.width*3/4,Screen.height);
		errorPanel = new Rectangle(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height);
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
		GUI.DrawTexture(editorPanel.getRect(),left_panel_background);
		//GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
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
				
		logKeyboardActivity();
		
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
		
		ProgramLogger.LogKVtime("delete", "all");
		ProgramLogger.LogKV("close", getSpellName()+", "+Time.time);
		ProgramLogger.LogCode(getSpellName(), current_code);
		
		if(IDEClosed != null) {
			
			IDEClosed(file_name, current_code);
			
		}
	}
		
	
	void detectKeyboardActivity()
	{
		if (GUI.changed || changed)
		{
		    if (inputThread == null || !inputThread.IsAlive) {
		        changed = false;
                Debug.Log("Starting thread");
                inputThread = new Thread (inputProcessing);
                inputThread.Start();
			} else
			    changed = true;
		}
	}
	
	
	void logKeyboardActivity()
	{
	    if (GUI.changed) {
		    clipboard = (string) typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null,null);
		    
		    Event e = Event.current;
		    if ( ((int)e.character) != 0 && String.Equals("None", e.keyCode+"") ) {  //type a character
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("insert", stateObj.pos-1, stateObj.pos, e.character+"");
		        else {
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		            ProgramLogger.LogEdit("insert", stateObj.pos-1, stateObj.pos, e.character+"");
		        }
		    } else if ((e.functionKey && String.Equals(e.keyCode+"", "Backspace")) || (String.Equals(""+e.keyCode, "X") && (e.command || e.control))) {   //delete or cut
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("remove", stateObj.pos, selStart, "");
		        else
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		    } else if ((e.command || e.control) && String.Equals(""+e.keyCode, "V")) {    //paste
		        if (selStart == selStop)
		            ProgramLogger.LogEdit("insert", stateObj.pos-clipboard.Length, stateObj.pos, clipboard);
		        else {
		            ProgramLogger.LogEdit("remove", selStart, selStop, selected);
		            ProgramLogger.LogEdit("insert", stateObj.pos-clipboard.Length, stateObj.pos, clipboard);
		        }
		    }
		}
		
		selected = stateObj.SelectedText+"";
		selStart = Math.Min(stateObj.pos, stateObj.selectPos);
		selStop = Math.Max(stateObj.pos, stateObj.selectPos);

	}
	
	
	void logNewErrors()
	{
	    if (! String.Equals(current_error, last_error)) {
            char[] trimchars = {'-', '\n', ' '};
            string cleanerror = current_error.Trim(trimchars);
            if (!String.IsNullOrEmpty(cleanerror))
                ProgramLogger.LogError(cleanerror);
            last_error = current_error;
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
		ProgramLogger.LogEdit("insert", GetCursorPosition(), GetCursorPosition()+tab_width, spaces);
		
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
	    GUI.DrawTexture(editorPanel.getRect(),left_panel_background);
		//GUI.DrawTexture(new Rect(0,0,Screen.width*3/4,Screen.height),left_panel_background);
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
	    GUI.BeginGroup(errorPanel.getRect());
		//GUI.BeginGroup(new Rect(Screen.width*3/4+5,0,Screen.width*1/4-5,Screen.height));
		if (GUI.Button (new Rect (10,15,130,65), "Back", button_style))
		{
			if (!shouldPopup) {
				//back button will change the SpellBook page name
	        	input.SetCode(current_code);
				
				enabled = false;
	        	previous_state.active = true;
				paused = true;
				Time.timeScale = 1;
				
				ProgramLogger.LogKV("close", getSpellName()+", "+Time.time);
				ProgramLogger.LogCode(getSpellName(), current_code);
					
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
		logNewErrors();
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

#endif
