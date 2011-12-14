/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class DraggableItem : Item {
	
	public override void ActiveInInventory()
	{
		if(Input.GetMouseButton(0))
		{
			Drag();
		} else {
			Drop();
		}
	}
	
	virtual protected void Drag()
	{
		SetHidden(true);
		getInventory().SetDragged(gameObject);
	}
	
	virtual protected void Drop()
	{
		SetActive(false);
		getInventory().SetDragged(null);


	    if(getInventory().MouseOverInventory()) // If we're back in the inventory
	    {
			DroppedOnInventory(Input.mousePosition);
        }else{ // If we're in the world
        	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	    	RaycastHit rayhit = new RaycastHit();
	        if(Physics.Raycast(ray,out rayhit))
	        	DroppedOn(rayhit.transform.gameObject);
	        else
	        	DroppedOn(null);
	     }
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
