/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class DraggableItem : Item {
	
	public override void ActiveInInventory()
	{
		if(Input.GetMouseButton(0))
		{
		    if (!(getInventory().DraggingOtherItem(gameObject)))
			    Drag();
		} else {
			Drop();
		}
	}
	
	virtual protected void Drag() //drag to inventory (any draggable gameObject)
	{
		SetHidden(true);
		getInventory().SetDragged(gameObject);
	}
	
	virtual protected void Drop()
	{
	    // make "uncastable" (uncompilable) spells not draggable
        if (this.GetType() == typeof(CodeScrollItem) && !((CodeScrollItem)this).IsCompilable()) {
            SetActive(false);
            if(getInventory().MouseOverInventory())
                DroppedOnInventory(Input.mousePosition);
            return;
        }
		SetActive(false);
		getInventory().SetDragged(null);
		
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
