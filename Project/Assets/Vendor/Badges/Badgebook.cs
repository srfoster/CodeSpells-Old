using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/**
 * 
 *  Okay, folks.  A pattern is emerging here, and we need to do some refactoring.
 *  The Inventory, Spellbook, and Badgebook all contain very similar structures:
 * 
 *  1) Display of itemized data.
 *  2) Storing of itemized data.
 *  3) Pagination and tabulation of the data.
 *  4) Game state transition (i.e. from playing to viewing the spellbook)
 *  5) Interaction with items (i.e. buttons.
 * 
 *  
 *  Each of these things is mashed together into Inventory, and independently mashed together in Spellbook.
 *  
 *  We should refactor what is common.
 * 
 *  We should create good primitives for tabulation and pagination.
 * 
 *  And we most definitely need to separate the data storage from the data display.
 *
 * */
public class Badgebook : MonoBehaviour {
	
	public delegate void EventHandler(BadgeStore.BadgeInfo badge);
	public static event EventHandler BadgeUnlocked;

	public Texture2D background;
	GameObject previous_state;
	
	public GUIStyle button_style = new GUIStyle();
	private GUIStyle empty_style = new GUIStyle();
		
	public Texture2D button_up_texture;
	public Texture2D button_down_texture;
	
	public Texture2D prev_button_texture;
	public Texture2D next_button_texture;
	private int current_page = 0;
	
	private GUIStyle unlock_button_style = new GUIStyle();
	public bool showOrangeBorder = false;
	private bool unlockPopup = false;
	private string unlockName = "";
	private string pword = "";
	
	public Font font;
	
	private List<int> columnHeights = new List<int>();

	bool enabled = false;
		
	public BadgeStore badgeStore = new BadgeStore();
	
	GUIStyle label_style;
	GUIStyle icon_style;
	
	int getNumberOfPages() {
	    return columnHeights.Count / 2 + columnHeights.Count % 2;
	    //return System.ToInt64(System.Math.Ceiling(System.ToDouble(columnHeights.Count) / 2)) ;
	}
	
	void OnGUI(){
		if(enabled){
			displayPages();
			
			if (GUI.Button (new Rect (Screen.width - 200,30,130,65), "Back", button_style))
			{	
				enabled = false;
	        	previous_state.active = true;
	    	}
	    	if (GUI.Button (new Rect (Screen.width - 135,110,65,65), "", unlock_button_style)) {
	    	    showOrangeBorder = !showOrangeBorder;
	    	    TraceLogger.LogKVtime("unlockbutton", ""+showOrangeBorder);
	    	}
	    	
	    	
	    	GUIStyle prev_button_style = new GUIStyle();
			prev_button_style.normal.background = prev_button_texture;
			if(current_page != 0 && GUI.Button (new Rect (Screen.width * 0.025f, Screen.height * 0.5f, 35, 35), "", prev_button_style))
			{
				current_page--;
				//PageTurnedBackward(currentPage());
				//logCurrentPage();
			}
			
			
			GUIStyle next_button_style = new GUIStyle();
			next_button_style.normal.background = next_button_texture;

			if(current_page != getNumberOfPages() - 1 && GUI.Button (new Rect (Screen.width * 0.95f, Screen.height * 0.5f, 35, 35), "", next_button_style))
			{
				current_page++;
				//PageTurnedForward(currentPage());
				//logCurrentPage();
			}
	    	
	    	
	    	// make it so that we can't click through to the game
			// NOTE: This must appear LAST in the OnGUI. Otherwise, other buttons won't work!
		    GUI.Button(new Rect(0,0,Screen.width,Screen.height), "", empty_style);
		}	
	}
	
	void Start(){
		button_style.normal.background = button_up_texture;
		button_style.active.background = button_down_texture;
		button_style.normal.textColor = Color.white;
		button_style.active.textColor = new Color(0.75f,0.75f,0.75f);
		button_style.alignment = TextAnchor.MiddleCenter;
		button_style.fontSize = 30;	
				
		label_style = new GUIStyle();
		label_style.fontSize = 20;
		label_style.font = font;
		label_style.normal.textColor = Color.black;
		label_style.alignment = TextAnchor.MiddleLeft;
		label_style.wordWrap = true;
		
		icon_style = new GUIStyle();
		
		unlock_button_style.normal.background = Resources.Load("unlock") as Texture2D;
		unlock_button_style.active.background = Resources.Load("unlock_darker") as Texture2D;
		
		//Spellbook spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
		prev_button_texture = Resources.Load("WoodenLeftArrow") as Texture2D; //spellbook.prev_button_texture;
		next_button_texture = Resources.Load("WoodenRightArrow") as Texture2D; //spellbook.next_button_texture;

	}
	
	public void Add(string name, string label, string icon_path, bool popup)
	{
	    badgeStore.Add(name, label, icon_path);	
		
		if(popup)
		{
			Popup.mainPopup.popup("Badge found!");	
		}
	}
	
	public void Replace(string name, string new_name, string label, string path, bool popup){
		badgeStore.Replace(name, new_name, label, path);
			
		if(popup)
		{
			Popup.mainPopup.popup("Badge unlocked!");
			BadgeUnlocked(badgeStore.Get(new_name));
		}
	}
	
	public void MakeButtonUnlockable(string name) {
	    badgeStore.Get(name).MakeButtonUnlockable();
	}
	
	public bool MarkAlreadyComplete(string name) {
	    if(!Contains("complete_"+name))
		{
		    if (badgeStore.label(name).Trim() == "")
		        Replace(name, "complete_" + name, badgeStore.label(name), badgeStore.path(name).Replace("incomplete","complete"), false);
		    else
			    Replace(name, "complete_" + name, badgeStore.label(name) + " (COMPLETED)", badgeStore.path(name).Replace("incomplete","complete"), false);
			TraceLogger.LogKV("alreadycompleted", name);
			return true;
		}
		return false;
	}
	
	public bool Complete(string name)
	{
		if(!Contains("complete_"+name))
		{
		    if (badgeStore.label(name).Trim() == "")
		        Replace(name, "complete_" + name, badgeStore.label(name), badgeStore.path(name).Replace("incomplete","complete"), true);
		    else
			    Replace(name, "complete_" + name, badgeStore.label(name) + " (COMPLETED)", badgeStore.path(name).Replace("incomplete","complete"), true);
			TraceLogger.LogKVtime("completed", name);
			BadgeLogger.Log(name);
			return true;
		} else {
			return false;
		}
		
	}
	
	public bool IsComplete(string name)
	{
		if(Contains("complete_"+name))
			return true;
		
		return false;
	}
	
	public bool Contains(string name)
	{
		return badgeStore.Contains(name);	
	}
	
	public void AddColumn(int height) {
	    columnHeights.Add(height);
	}
	
	void displayPages()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
		
		
		int table_width = (int) (Screen.width * .45);
		int table_height = (int) (Screen.height * .8);
		
		int left = current_page * 2 + 1;
		int right = current_page * 2 + 2;
		

		GUI.BeginGroup(new Rect(50f,30f, table_width, table_height));

		displayPage(left, table_width, table_height);		

		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(50f + table_width + 100, 30f, table_width, table_height));
        
        if (right < columnHeights.Count)
		    displayPage(right, table_width, table_height);		

		GUI.EndGroup();
		
		if (unlockPopup)
		    tryUnlock();
	}
	
	void displayPage(int page_number, int table_width, int table_height)
	{
		//Would be nice to have an abstract GUI utility for doing this kind of stuff.
		
		int row = 0;
		int col = 0;
		
		int field_width  = (int) (Screen.width * .40f);
		int field_height = 50;
		
		int per_page = columnHeights[page_number-1]; //(table_width / field_width) * (table_height / field_height);
		int page_max = 0;//columnHeights.Take(page_number).Sum(); //per_page; // * page_number;
		for (int i=0; i<page_number-1; i++)
		    page_max += columnHeights[i];
		
		int height_so_far = 0;
				
		for(int i = page_max /*- per_page*/; i < Mathf.Min(badgeStore.Size(), page_max+per_page); i++)
		{					
			int x = col * field_width;
			int y = row * field_height;
			
			icon_style.normal.background = badgeStore.icon(i);
			GUI.Box(new Rect(x,y, 50 ,50), "" , icon_style);
			
			if(badgeStore.path(i).Contains("incomplete"))
			{
				label_style.normal.textColor = Color.gray;	
			} else {
				label_style.normal.textColor = Color.black;	
			}
            
            // make the name a button
            if ((badgeStore.Get(i)).buttonUnlockable && !unlockPopup) {
                if (GUI.Button(new Rect(x + 55, y, Screen.width * .40f - 50, 50), badgeStore.label(i), label_style)) {
                    unlockPopup = true;
                    unlockName = badgeStore.name(i);
                    Debug.Log(unlockName+" clicked");
                }
            } else
			    GUI.Label(new Rect(x + 55, y, Screen.width * .40f - 50, 50), badgeStore.label(i), label_style);
			
			height_so_far += field_height;
			row++;
			
			if(height_so_far > table_height)
			{
				row = 0;
				col++;
				
				height_so_far = 0;
			}
		}	
	}
	
	private void tryUnlock() {
	    int boxWidth = 300;
        int boxHeight = 115;
        GUIStyle style = GUI.skin.box;
        style.alignment = TextAnchor.UpperCenter;
        GUI.Box(new Rect (Screen.width/2-boxWidth/2, Screen.height/2-boxHeight/2, boxWidth, boxHeight), "Do you want to unlock\n"+unlockName+"?", style);
        
        pword = GUI.PasswordField(new Rect(Screen.width/2-100, Screen.height/2-15, 200, 20), pword, "*"[0]);
        
        
        if (GUI.Button(new Rect(Screen.width/2-60, Screen.height/2+15, 50, 30), "Yes")) {
            if (pword == "LaJo11a")
                Complete(unlockName);
            else
                TraceLogger.LogKVtime("unlockbutton", "Yes");
            unlockPopup = false;
            unlockName = "";
            showOrangeBorder = false;
            pword = "";
        }
        
        if (GUI.Button(new Rect(Screen.width/2+30, Screen.height/2+15, 50, 30), "No")) {
            unlockPopup = false;
            unlockName = "";
            showOrangeBorder = false;
            pword = "";
            TraceLogger.LogKVtime("unlockbutton", "No");
        }
	}
	
		
	public void show(GameObject previous_state)
	{
		this.previous_state = previous_state;
		previous_state.active = false;
		enabled = true;
	}

}
