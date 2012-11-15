using UnityEngine;
using System.Collections;

public class PracticeRoomSpellbookItem : DraggableItem {
	
	void Start(){
		inventoryTexture = Resources.Load("SpellbookIcon") as Texture2D;	
	}
	
    override protected void Drag(){

	}

	public override void DroppedOnInventory(Vector3 mousePosition){
		SetHidden(false);

		PracticeRoomSpellbook spellbook = (GameObject.Find("PracticeRoomSpellbook").GetComponent<PracticeRoomSpellbook>());

		spellbook.show(GameObject.Find("Inventory"));
	}

}
		 
