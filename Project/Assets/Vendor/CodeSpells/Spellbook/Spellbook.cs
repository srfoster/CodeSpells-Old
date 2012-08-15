using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spellbook : MonoBehaviour {
	
			
	public delegate void EventHandler(SpellbookPage page);
	public static event EventHandler PageTurnedForward;
	public static event EventHandler PageTurnedBackward;
	public static event EventHandler SpellCopied;

	
	Texture2D background_texture;
	
	GameObject previous_state;
	
	bool enabled = false;
	
	public GUIStyle button_style = new GUIStyle();
	
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	
	public Texture2D prev_button_texture;
	public Texture2D next_button_texture;
	public Texture2D copy_button_texture;
	
	private Vector2 scroll_position = Vector2.zero;
	
	public Font code_font;
	
	IDE ide;
	
	int current_page = 0;
	
	public List<string> page_urls = new List<string>();
	public List<SpellbookPage> pages = new List<SpellbookPage>();
	
	Dictionary<string, int> copied_spells = new Dictionary<string,int>();

	
	IEnumerator Start()
	{
		background_texture = Resources.Load("SpellbookMock") as Texture2D;
		
			
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;
		
		ide = GameObject.Find("IDE").GetComponent<IDE>();
				
		
		foreach(string url in page_urls)
		{
			//Gotta parse out the name
			string[] split = url.Split(new char[]{'/'});
			string name = split[split.Length - 1];
			
			
			string new_url = "";
			
			for(int i = 0; i < split.Length-1; i++)
			{
				new_url += split[i] + "/";
			}
			
			SpellbookPage page = new WebSpellbookPage(new_url, name);
			pages.Add(page);
			StartCoroutine(page.load());
		}
		
		
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
			
			GUIStyle copy_button_style = new GUIStyle();
			copy_button_style.normal.background = copy_button_texture;
			copy_button_style.normal.textColor = Color.black;
			copy_button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
			copy_button_style.alignment = TextAnchor.MiddleCenter;
			copy_button_style.fontSize = 30;
			
			if (GUI.Button (new Rect (Screen.width * 0.17f, Screen.height * 0.86f, 210, 50), "Copy", copy_button_style)){
				enabled = false;
	        	previous_state.active = true;
				
				givePlayerAScroll();
				
				SpellCopied(currentPage());
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
	
	void givePlayerAScroll()
	{
		CodeScrollItem item;

		GameObject initial_scroll = new GameObject();
		initial_scroll.name = "InitialScroll";
		initial_scroll.AddComponent<CodeScrollItem>();
		item = initial_scroll.GetComponent<CodeScrollItem>();
		item.item_name = "Blank";
		item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
				
		if(!copied_spells.ContainsKey(currentPage().getName()))
			copied_spells.Add(currentPage().name,0);
		
		
		int number_so_far = copied_spells[currentPage().getName()];
		int number = number_so_far + 1;
		

		
		copied_spells[currentPage().name]++;
		

		Debug.Log("HERE " + number);

		CodeScrollItem code_scroll_item_component = initial_scroll.GetComponent<CodeScrollItem>();
		code_scroll_item_component.setCurrentFile(currentPage().getName() + number + ".java");
		
		code_scroll_item_component.getIDEInput().SetCode(currentPage().code.Replace(currentPage().getName(), currentPage().getName() + number));

		GameObject.Find("Inventory").GetComponent<Inventory>().addItem(initial_scroll);
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
		IDEInput input = new StringInput(currentPage().code, "");
		ide.noEdit();
		ide.SetInput(input);


		GUILayout.BeginArea(new Rect(Screen.width * 0.07f, Screen.height * 0.14f, Screen.width * 0.383f, Screen.height * 0.725f));
		scroll_position = GUILayout.BeginScrollView (scroll_position, GUILayout.Width(Screen.width * 0.383f), GUILayout.Height(Screen.height * 0.725f)); // Should vary the size of the last rect by how much text we have??
		ide.showCode();
		ide.syntaxHighlight();
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
