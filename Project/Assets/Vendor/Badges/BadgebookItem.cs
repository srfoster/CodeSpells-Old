using UnityEngine;
using System.Collections;

public class BadgebookItem : DraggableItem {
	
	void Start(){
		inventoryTexture = Resources.Load("BadgebookIcon") as Texture2D;	
	}
	
    override protected void Drag(){

	}

	public override void DroppedOnInventory(Vector3 mousePosition){
		SetHidden(false);

		Badgebook badgebook = (GameObject.Find("Badgebook").GetComponent<Badgebook>());
		badgebook.show(GameObject.Find("Inventory"));
	}

}
		 
