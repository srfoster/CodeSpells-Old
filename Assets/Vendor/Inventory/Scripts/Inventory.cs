/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public Texture2D inventory_area_background;
	
	public Texture2D up_button_texture;
	public Texture2D down_button_texture;
		
	public Font label_font;
	
	
	private ArrayList items = new ArrayList();
	private ArrayList to_remove = new ArrayList();
	
	private int margin_top  = 50;
	private int margin_left_side = 20;
	private int margin_right_side = 70;
	private int margin_bottom = 30;
	
	private int vertical_spacing = 20;
	
	private int inventory_width = 320;
	
	private int item_width = 50;
	private int item_height = 50;
	
	private int item_padding = 10;
	
	private int label_height = 40;
	
	private int item_columns = 3;
	
	private float vSliderValue = 0.0f;
	
	private int starting_row = 0;
	
	private bool overflow_bottom = false;
	private bool overflow_top = false;
	
	private GameObject dragged = null;
	
	private GUIStyle item_label_style;
	
	void Start(){
		item_label_style = new GUIStyle();
		item_label_style.normal.textColor = Color.white;
		item_label_style.alignment = TextAnchor.MiddleCenter;
		item_label_style.fontSize = 20;
		item_label_style.wordWrap = true;
		item_label_style.font = label_font;
	}
	
	void Update(){
		notifyActive();
	}
	
	public void SetDragged(GameObject item)
	{
		dragged = item;
	}
	
	public bool MouseOverInventory()
	{
		return (new Rect(Screen.width - inventory_width,0,inventory_width,Screen.height)).Contains(Input.mousePosition);
	}
	
	void OnGUI () {
	
		GUI.BeginGroup(new Rect(Screen.width - inventory_width,0,inventory_width,Screen.height));
	
		//Draw Background
		GUI.DrawTexture(new Rect(0,0,inventory_width,Screen.height),inventory_area_background);
		
		displayItems();
		displayButtons();
		
			
		GUI.EndGroup();
		
		displayDragged();
	
	}
	
	void displayDragged(){
		if(dragged == null)
			return;
					
		var x = Input.mousePosition.x - item_width/2;
		var y = Screen.width/2 - Input.mousePosition.y - item_height/2;
	
		GUI.DrawTexture(new Rect(x,y,item_width,item_height),(dragged.GetComponent(typeof(Item)) as Item).getTexture());
		
		GUI.Label(new Rect(x, y + item_height, item_width, label_height), (dragged.GetComponent(typeof(Item)) as Item).getName(), item_label_style);
	}
	
	
	void displayItems()
	{
		var column = 0;
		var row = 0;
		
		overflow_bottom = false;
		overflow_top    = false; 
		
		foreach(GameObject r in to_remove)
		{
			items.Remove(r);
		}
		
		to_remove.Clear();
		
		foreach(GameObject item in items)
		{
			float item_padding = (1.0f * inventory_width - margin_right_side - margin_left_side - (item_columns * item_width)) / (item_columns);
			float item_x = margin_right_side + column * (item_width + item_padding);
			float item_y = margin_top + row * (item_height + vertical_spacing + label_height) - (starting_row * (item_height + vertical_spacing)) ;
			
			
			if(item_y + item_height + label_height < Screen.height - margin_bottom)
			{
				if(item_y >= margin_top)
				{
					GUIStyle item_button_style = new GUIStyle();
					item_button_style.normal.background = (item.GetComponent(typeof(Item)) as Item).getTexture();
					
					if(!(item.GetComponent(typeof(Item)) as Item).GetHidden())
					{
						bool item_clicked = false;
						item_clicked = GUI.RepeatButton(new Rect(item_x,  item_y,item_width,item_height), "", item_button_style);
										
						GUI.Label(new Rect(item_x, item_y + item_height, item_width, label_height), (item.GetComponent(typeof(Item)) as Item).getName(), item_label_style);
		
						
						if(item_clicked)
						{
							itemClicked(item);
						}
					}
				} else {
					overflow_top = true;
				}
			} else {
				overflow_bottom = true;
			}
			
			
			
			column++;
			column = column % item_columns;
			if(column == 0)
				row++;
	
		}
	}
	
	void displayButtons()
	{
		int button_width = 35;
		int button_height = 35;
		int button_margin = 3;
		
		if(overflow_top)
		{
			GUIStyle up_button_style = new GUIStyle();
		
			up_button_style.normal.background = up_button_texture;
	
			if(GUI.Button(new Rect(inventory_width/2 - button_width/2,button_margin, button_width, button_height), "", up_button_style))
			{
				if(starting_row > 0)
					starting_row--;
			}
		}
		
		if(overflow_bottom)
		{
			GUIStyle down_button_style = new GUIStyle();
			
			down_button_style.normal.background = down_button_texture;
		
			
			if(GUI.Button(new Rect(inventory_width/2 - button_width/2,Screen.height-button_height-button_margin,button_width, button_height), "", down_button_style))
			{
				starting_row++;
			}
		}
	}
	
	
	public void addItem(GameObject item)
	{
		items.Add(item);
	}
	
	
	public void removeItem(GameObject item)
	{
		to_remove.Add(item);
	}
	
	public void disactivate(GameObject item)
	{
		(item.GetComponent(typeof(Item)) as Item).SetActive(false);
	}
	
	public void itemClicked(GameObject item)
	{
		if((item.GetComponent(typeof(Item)) as Item).GetActive())
			return;
		
		(item.GetComponent(typeof(Item)) as Item).ClickedInInventory();
	
		(item.GetComponent(typeof(Item)) as Item).SetActive(true);	
	}
	
	void notifyActive()
	{
		foreach(GameObject item in items)
		{
			if((item.GetComponent(typeof(Item)) as Item).GetActive())
				(item.GetComponent(typeof(Item)) as Item).ActiveInInventory();
		}
	}

}
