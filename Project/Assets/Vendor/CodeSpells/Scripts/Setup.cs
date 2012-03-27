using UnityEngine;
using System.Collections;

public class Setup : MonoBehaviour {
	CodeScrollItem item;
	
	// Use this for initialization
	void Start () {		
		givePlayerAScroll();
		
		addEnchantments();
	}
	
	void addEnchantments(){
		GameObject target1 = GameObject.Find("LevitateFloatingBox");
		GameObject target2 = GameObject.Find("MoveFloatingBox");
		GameObject target3 = GameObject.Find("TransportFloatingBox");
		
		June june1 = new JuneWithDefault(target1, "Levitate.java", Application.dataPath + "/Vendor/CodeSpells/JavaSourceFiles/Levitate.java");
		(target1.GetComponent("Enchantable") as Enchantable).enchant(june1, delegate(GameObject t){item.absorbSpell(t); });
		
		June june2 = new JuneWithDefault(target2, "Move.java", Application.dataPath + "/Vendor/CodeSpells/JavaSourceFiles/Move.java");
		(target2.GetComponent("Enchantable") as Enchantable).enchant(june2, delegate(GameObject t){item.absorbSpell(t); });
		
		June june3 = new JuneWithDefault(target3, "Transport.java", Application.dataPath + "/Vendor/CodeSpells/JavaSourceFiles/Transport.java");
		(target3.GetComponent("Enchantable") as Enchantable).enchant(june3, delegate(GameObject t){item.absorbSpell(t); });
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
