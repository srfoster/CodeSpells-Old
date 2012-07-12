using UnityEngine;
using System.Collections;

public class SetupLevel : MonoBehaviour {
	CodeScrollItem item;
	
	void Start()
	{
		if(Application.isPlaying)
			Init();	
	}
	
	bool done = false;
	void Update()	
	{
		if(!done)
		{
			done = true;

		}
	}
	
	public void Init() {
		(new SetupInventory()).Init();		
		(new SetupUnidee()).Init();		
		(new SetupPopup()).Init();		
		(new SetupJune()).Init();		
		(new SetupConversations()).Init();		
		(new SetupHighlighter()).Init();
		(new SetupSpellbook()).Init();
		(new SetupBadgebook()).Init();

		
		ObjectManager.Register(GameObject.Find("First Person Controller"), "Me");
		
		givePlayerASpellbook();
		givePlayerABadgeBook();
	//	givePlayerAScroll();
	}
	
	void givePlayerASpellbook()
	{
		GameObject book = new GameObject();
		book.name = "Book";
		book.AddComponent<SpellbookItem>();
		SpellbookItem item = book.GetComponent<SpellbookItem>();
		item.item_name = "Spells";
				
		Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		inventory.addItem(book);
		
		Spellbook spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flame");

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Levitate");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/AdeptLevitate");

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Teleport");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flight");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Summon");
		
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MassiveFire");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MassiveFire2");

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Architecture");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Architecture2");

	}
	
	void givePlayerABadgeBook()
	{
		GameObject book = new GameObject();
		book.name = "Badges";
		book.AddComponent<BadgebookItem>();
		BadgebookItem item = book.GetComponent<BadgebookItem>();
		item.item_name = "Badges";
				
		Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		inventory.addItem(book);
		
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		
		
		badgebook.Add("helping_others", 				"HELPING OTHERS", 				"test_badge", false);
		badgebook.Add("helping_others_cross_river", 	"  Cross River", 				"test_badge", false);
		badgebook.Add("helping_others_light_fire", 		"  Light Fire", 				"test_badge", false);
		badgebook.Add("helping_others_make_bread", 		"  Make Bread", 				"test_badge", false);
		
		
		badgebook.Add("reading_your_book", 					"READING YOUR BOOK", 			"test_badge", false);
		badgebook.Add("reading_your_book_fire", 			"  Flames", 					"test_badge", false);
		badgebook.Add("reading_your_book_levitate", 		"  Novice Levitation", 			"test_badge", false);
		badgebook.Add("reading_your_book_adv_levitate", 	"  Adept Levitation", 			"test_badge", false);
		badgebook.Add("reading_your_book_flight", 			"  Flight", 					"test_badge", false);
		badgebook.Add("reading_your_book_summon", 			"  Summoning", 					"test_badge", false);
		badgebook.Add("reading_your_book_massive", 			"  Massive Fire", 				"test_badge", false);
		badgebook.Add("reading_your_book_architecture", 	"  Architecture", 				"test_badge", false);

		/*
		badgebook.Add("learning_the_craft", 				"Learning the Craft", 							"test_badge", false);
		badgebook.Add("learning_the_craft_first_mod", 		"  First Spell Modification", 					"test_badge", false);
		badgebook.Add("learning_the_craft_first_loop", 		"  First Loop Modification", 					"test_badge", false);
		badgebook.Add("learning_the_craft_first_if", 		"  First Conditional Modification", 			"test_badge", false);
		badgebook.Add("learning_the_craft_first_list", 		"  First List", 								"test_badge", false);
		*/
		

		
		
		
		//badgebook.Replace("helping_others_light_fire", "completed_helping_others_light_fire", "  Light Fire", "new_icon_name", true);
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
		
		Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
		inventory.addItem(initial_scroll);	
		
		CodeScrollItem code_scroll_item_component = initial_scroll.GetComponent<CodeScrollItem>();
		code_scroll_item_component.setCurrentFile("Levitate.java");
	}

}
