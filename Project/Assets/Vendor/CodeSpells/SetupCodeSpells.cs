using UnityEngine;
using System.Collections;

public class SetupCodeSpells : MonoBehaviour {
	CodeScrollItem item;
	
	public SetupCodeSpells()
	{

	}
	
	void Start()
	{
		if(Application.isPlaying)
			Init();	
	}
	
	public void Init() {
		(new SetupBasicWorld()).Init();		
		(new SetupInventory()).Init();		
		(new SetupUnidee()).Init();		
		(new SetupPopup()).Init();		
		(new SetupJune()).Init();		
		(new SetupConversations()).Init();		

		
		GameObject prefab = Resources.Load("Camp") as GameObject;
		
		GameObject obj = Fancy.InstantiatePrefab(prefab, new Vector3(3.633766f,-0.6606301f,17.04282f), Quaternion.identity) as GameObject;
		obj.name = prefab.name;
		
		
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
		initial_scroll.name = "InitialScroll";
		initial_scroll.AddComponent("CodeScrollItem");
		item = (initial_scroll.GetComponent("CodeScrollItem") as CodeScrollItem);
		item.item_name = "Blank";
		item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
		
		GameObject test = GameObject.Find("Inventory");
		Debug.Log("TEST2.... " + test);

		
		Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
		inventory.addItem(initial_scroll);	
	}
	

}
