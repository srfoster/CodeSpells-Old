using UnityEngine;
using System.Collections;

public class PickUpableItem : DraggableItem {
	
	private bool onDrop = false;
	private int rectWidth = System.Convert.ToInt32((Screen.width-300)/2.0);
	private int rectHeight = System.Convert.ToInt32(Screen.height/2.0);
	
	void OnGUI() {
		if (onDrop) {
			item_name = GUI.TextField(new Rect (rectWidth, rectHeight, 140,20), item_name, 16);
		}
		
		Event e = Event.current;

        if (e.keyCode == KeyCode.Return) {
			onDrop = false;
			
			gameObject.GetComponent<Enchantable>().setId(item_name);
		}
	}
	
	public override void DroppedOn(GameObject target)
	{	
		//get spawning zone
		GameObject spawningZone = GameObject.Find ("Spawning Zone");
		getInventory().removeItem(transform.gameObject);
		Vector3 nextPosition = new Vector3(spawningZone.transform.position.x, Terrain.activeTerrain.SampleHeight(spawningZone.transform.position),spawningZone.transform.position.z);
		transform.position = nextPosition;
	}
	
	public override void DroppedOnInventory(Vector3 mousePosition)
	{
		onDrop = true;
		Debug.Log ("onDrop is "+onDrop);
		
		SetHidden(false);	
	}
}
