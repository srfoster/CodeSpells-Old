using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PracticeRoomSpellbook : MonoBehaviour {
	
			
	public delegate void EventHandler(SpellbookPage page);
	public static event EventHandler PageTurnedForward;
	public static event EventHandler PageTurnedBackward;

	Texture2D background_texture;
	
	GameObject previous_state;
	
	bool enabled = false;
	
	public GUIStyle button_style = new GUIStyle();
	public GUIStyle code_style = new GUIStyle();
	
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	
	public Texture2D prev_button_texture;
	public Texture2D next_button_texture;
	
	private Vector2 scroll_position = Vector2.zero;
	
	public Font code_font;
	
	IDE ide;
	
	int current_page = 0;
	
	public List<SpellbookPage> pages = new List<SpellbookPage>();
	
	IEnumerator Start()
	{
		background_texture = Resources.Load("SpellbookMock") as Texture2D;
		
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;
		
		//set code_style
		code_style.fontSize = 20;
		code_style.normal.textColor = Color.black;
		code_style.font = code_font;
		code_style.wordWrap = false;
		
		ide = GameObject.Find("IDE").GetComponent<IDE>();
		
		return null;
	}
	
	public void Add(SpellbookPage page)
	{
		pages.Add(page);	
	}
	
	void OnGUI(){
		if(enabled){
			displayCurrentPage();

			if (GUI.Button (new Rect (Screen.width - 200,30,130,65), "Back", button_style))
			{	
				enabled = false;
	        	previous_state.active = true;
	    	}
			
			GUIStyle prev_button_style = new GUIStyle();
			prev_button_style.normal.background = prev_button_texture;
			if(current_page != 0 && GUI.Button (new Rect (Screen.width * 0.025f, Screen.height * 0.5f, 35, 35), "", prev_button_style))
			{
				current_page--;
				PageTurnedBackward(currentPage());
			}
			
			GUIStyle next_button_style = new GUIStyle();
			next_button_style.normal.background = next_button_texture;
			if(current_page != pages.Count - 1 && GUI.Button (new Rect (Screen.width * 0.95f, Screen.height * 0.5f, 35, 35), "", next_button_style))
			{
				current_page++;
				PageTurnedForward(currentPage());
			}
		}
	}
	
	void displayCurrentPage()
	{
		if(currentPage() == null || currentPage().texture == null || currentPage().code == null)
		{
			Debug.Log("Current " + currentPage().texture + " " + currentPage().code);
			
			enabled = false;
			previous_state.active = true;

			return;	
		}
			
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),currentPage().texture);
		
		if(!currentPage().code.Equals(""))
			showCode();
	}
	
	void showCode()
	{	
		GUILayout.BeginArea(new Rect(Screen.width * 0.07f, Screen.height * 0.14f, Screen.width * 0.383f, Screen.height * 0.725f));
		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width * 0.383f), GUILayout.Height(Screen.height * 0.725f)); // Should vary the size of the last rect by how much text we have??

		foreach (Tuple<Rect, Texture2D> tup in (new Highlight()).highlightPage(currentPage().code)) {
			GUI.DrawTexture(tup.Item1, tup.Item2);
		}
		GUILayout.TextArea(currentPage().code, code_style);
		
        GUILayout.EndScrollView();
		GUILayout.EndArea();
	}
	
	
	public void show(GameObject previous_state)
	{
		this.previous_state = previous_state;
		previous_state.active = false;
		enabled = true;
	}
	
	SpellbookPage currentPage(){
		return pages[current_page];	
	}
}
