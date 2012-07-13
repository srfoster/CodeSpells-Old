using UnityEngine;
using System.Collections;

public class PickUpableItem : DraggableItem {
	
	private bool onDrop = false;
	private bool has_set_focus = false;
	
	void OnGUI() {
		
		//Basically we draw a text field RIGHT on top of the inventory's label.
		//  It works, but totally by coincidence -- i.e. due to some weird, unstanted collaboration between this class and Inventory.
		if (onDrop) {
			float x = getInventory().getInfo(gameObject).label_x;
			float y = getInventory().getInfo(gameObject).label_y;
			
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = Color.white;
			style.font = Resources.Load("Erika Ormig") as Font;
			style.fontSize = 20;
			style.wordWrap = true;

			
			GUI.SetNextControlName("Rename Field");
			item_name = GUI.TextField(new Rect (x, y, 100,40), item_name, style);
			
			Event e = Event.current;

	        if (e.keyCode == KeyCode.Return) {
				onDrop = false;
				
				gameObject.GetComponent<Enchantable>().setId(item_name);
			}
			
			if(!has_set_focus){
				GUI.FocusControl("Rename Field");
				TextEditor stateObj = GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl) as TextEditor;
				stateObj.MoveRight();
				has_set_focus = true;
			}
		}
	}
	
	public override void DroppedOn(GameObject target)
	{	
		//get spawning zone
		onDrop = false;
		GameObject spawningZone = GameObject.Find ("Spawning Zone");
		getInventory().removeItem(gameObject);
		Vector3 nextPosition = new Vector3(spawningZone.transform.position.x, Terrain.activeTerrain.SampleHeight(spawningZone.transform.position),spawningZone.transform.position.z);
		transform.position = nextPosition;
		SetHidden(false);	
	}
	
	public override void DroppedOnInventory(Vector3 mousePosition)
	{
		onDrop = true;
		has_set_focus = false;
		SetHidden(false);	
	}
}
