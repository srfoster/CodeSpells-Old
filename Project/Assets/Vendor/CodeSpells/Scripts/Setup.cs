using UnityEngine;
using System.Collections;

public class Setup : MonoBehaviour {
	CodeScrollItem item;

	// Use this for initialization
	void Start () {
		(GameObject.Find("Popup").GetComponent("Popup") as Popup).popup("Todo: 1) Kill java somehow,\n 2) reset java code state,\n 3) enchantment distance restriction");
		
		givePlayerAScroll();
		
		addEnchantments();
	}
	
	void addEnchantments(){
		GameObject target = GameObject.Find("FloatingBox");
		
		June june = new June(target, "Levitate.java");

		(target.GetComponent("Enchantable") as Enchantable).enchant(june, delegate(GameObject t){item.absorbSpell(t); });
	}
	
	void givePlayerAScroll()
	{
		GameObject initial_scroll = new GameObject();
		initial_scroll.AddComponent("CodeScrollItem");
		item = (initial_scroll.GetComponent("CodeScrollItem") as CodeScrollItem);
		item.item_name = "Blank";
		item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
		Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
		inventory.addItem(initial_scroll);	
	}
	
}
