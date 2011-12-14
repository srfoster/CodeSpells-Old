/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	
	//The in-inventory representation of an object
	public Texture2D inventoryTexture;
	public string item_name;
	
	
	private bool is_active = false;
	private bool is_hidden = false;
	
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
		return GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
	}
	
	public Texture2D getTexture()
	{
		return inventoryTexture;
	}
	
	public string getName()
	{
		return item_name;
	}
	
	void OnMouseDown(){
		getInventory().addItem(gameObject);
				
		this.gameObject.SetActiveRecursively(false);
	}
	
	virtual public void ClickedInInventory()
	{
		Debug.Log("Clicked...");
	}
	
	virtual public void ActiveInInventory()
	{
	
	}
	

}
