/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour {

	
	//The in-inventory representation of an object
	public Texture2D inventoryTexture;
	public string item_name;
	
	private bool is_active = false;
	private bool is_hidden = false;
	public NoUnderground nucomp;
	
	public bool GetActive()
	{
		return is_active;
	}
	
	public void SetActive(bool b)
	{
		is_active = b;
	}
	
	public bool GetHidden()
	{
		return is_hidden;
	}
	
	public void SetHidden(bool b)
	{
		is_hidden = b;
	}
	
	void Start(){
	}
	
	public Inventory getInventory()
	{
		GameObject inventory = GameObject.Find("Inventory");
		
		if(inventory != null)
			return inventory.GetComponent(typeof(Inventory)) as Inventory;
		
		return null;
	}
	
	public virtual Texture2D getTexture()
	{
		return inventoryTexture;
	}
	
	public string getName()
	{
		return item_name;
	}
	
	virtual public void handleMouseDown()
	{
		if(!enabled)
			return;
		
		try{
			getInventory().addItem(gameObject);
			if (this.gameObject.GetComponent<NoUnderground>()) {
				nucomp = this.gameObject.GetComponent<NoUnderground>();
				nucomp.enabled = false;
			}
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,-10000f,this.gameObject.transform.position.z); //SetActiveRecursively(false);
		}catch(NullReferenceException e){
			
		}
	}
	
	
	void OnMouseDown()
	{
		handleMouseDown();
	}
	
	public bool isInInventory()
	{
		if(getInventory().getInfo(this.gameObject) != null)
			return true;	
		return false;
	}
	
	virtual public void ClickedInInventory()
	{
	}
	
	virtual public void ActiveInInventory()
	{
	
	}
	

}
