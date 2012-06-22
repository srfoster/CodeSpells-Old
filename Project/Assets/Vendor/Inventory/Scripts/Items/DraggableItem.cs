/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class DraggableItem : Item {
	
	private bool onDrop = false;
	private int rectWidth = System.Convert.ToInt32((Screen.width-300)/2.0);
	private int rectHeight = System.Convert.ToInt32(Screen.height/2.0);
	
	void OnGUI() {
		if (onDrop) {
			item_name = GUI.TextField(new Rect (rectWidth, rectHeight, 140,20), item_name, 16);
		}
	}
	
	public override void ActiveInInventory()
	{
		if(Input.GetMouseButton(0))
		{
			Drag();
		} else {
			Drop();
		}
	}
	
	virtual protected void Drag() //drag to inventory (any draggable gameObject)
	{
		Debug.Log ("inside drag");
		SetHidden(true);
		getInventory().SetDragged(gameObject);

	}
	
	virtual protected void Drop()
	{
		onDrop = true;
		Debug.Log ("onDrop is "+onDrop);
		SetActive(false);
		getInventory().SetDragged(null);
		
		//

	    if(getInventory().MouseOverInventory()) // If we're back in the inventory
	    {
			DroppedOnInventory(Input.mousePosition);
        }else{ // If we're in the world
        	GameObject obj = objectUnderCursor();
	        if(obj != null)
	        	DroppedOn(obj);
	        else
	        	DroppedOn(null);
	     }
	}
	
	protected GameObject objectUnderCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit = new RaycastHit();
		
		if(Physics.Raycast(ray, out hit))
			return hit.transform.gameObject;
		else 
			return null;
	}
	
	virtual public void DroppedOn(GameObject gameObject)
	{
		SetHidden(false);
	}
		
	virtual public void DroppedOnInventory(Vector3 mousePosition)
	{
		SetHidden(false);	
	}
	
}
