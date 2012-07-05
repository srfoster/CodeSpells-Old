using UnityEngine;
using System.Collections;

public class SpellbookItem : DraggableItem {
	
	void Start(){
		inventoryTexture = Resources.Load("SpellbookIcon") as Texture2D;	
	}
	
    override protected void Drag(){

	}

	public override void DroppedOnInventory(Vector3 mousePosition){
		SetHidden(false);

		Spellbook spellbook = (GameObject.Find("Spellbook").GetComponent<Spellbook>());
		spellbook.show(GameObject.Find("Inventory"));
	}

}
		 
