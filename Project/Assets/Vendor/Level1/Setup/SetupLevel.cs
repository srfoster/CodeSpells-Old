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
	
		badgebook.Add("helping_others", 				"HELPING OTHERS", 				"incomplete_helping_others_badge", false);
		badgebook.Add("helping_others_picking_up_item",	"  Pickin up Stuff", 			"incomplete_picking_up_item_badge", false);
		badgebook.Add("helping_others_light_fire", 		"  Light Fire", 				"incomplete_light_fire_badge", false);
		badgebook.Add("helping_others_cross_river", 	"  Cross River", 				"incomplete_crossing_river_badge", false);
		badgebook.Add("helping_others_reaching_up_high", "  Out of Reach", 				"incomplete_putting_something_high_badge", false);
		badgebook.Add("helping_others_putting_something_high", 	"  New Heights", 		"incomplete_reaching_up_high_badge", false);
		badgebook.Add("helping_others_put_out_fire", 	"  Firefighter", 				"incomplete_putting_out_fire_badge", false);
		badgebook.Add("helping_others_moving_rocks", 	"  Moving Cargo", 				"incomplete_moving_rocks_badge", false);
		
		

		badgebook.Add("blank_line_hack1", 					"", 			"", false);
		badgebook.Add("blank_line_hack2", 					"", 			"", false);
		

		
		badgebook.Add("reading_your_book", 					"READING YOUR BOOK", 			"incomplete_reading_your_book_badge", false);
		badgebook.Add("reading_your_book_fire", 			"  Flames", 					"incomplete_cast_flame_badge", false);
		badgebook.Add("reading_your_book_adv_levitate", 	"  Adept Levitation", 			"incomplete_cast_levitate_badge", false);
		badgebook.Add("reading_your_book_flight", 			"  Flight", 					"incomplete_cast_flight_badge", false);
		badgebook.Add("reading_your_book_teleport", 		"  Teleportation", 				"incomplete_cast_teleport_badge", false);
		badgebook.Add("reading_your_book_summon", 			"  Summoning", 					"incomplete_cast_summoning_badge", false);
		badgebook.Add("reading_your_book_massive", 			"  Massive Fire", 				"incomplete_cast_massive_fire_badge", false);
		badgebook.Add("reading_your_book_architecture", 	"  Architecture", 				"incomplete_cast_architecture_badge", false);
		
		
		//Set up the callbacks for unlocking the badges.
		
		
		Enchantable.EnchantmentEnded += (spell_target, item_name) => {
		
			if(item_name.StartsWith("Flame"))
				badgebook.Complete("reading_your_book_fire");
			
			if(item_name.StartsWith("Massive"))
				badgebook.Complete("reading_your_book_massive");
			
			if(item_name.StartsWith("Flight"))
				badgebook.Complete("reading_your_book_flight");
			
			if(item_name.StartsWith("AdeptLevitate"))
				badgebook.Complete("reading_your_book_adv_levitate");
			
			if(item_name.StartsWith("Summon"))
				badgebook.Complete("reading_your_book_summon");
					
			if(item_name.StartsWith("Teleport"))
				badgebook.Complete("reading_your_book_teleport");
			
			if(item_name.StartsWith("Architecture"))
				badgebook.Complete("reading_your_book_architecture");
			
		};
		
		
	
		/*
		badgebook.Add("learning_the_craft", 				"LEARNING THE CRAFT", 							"test_badge", false);
		badgebook.Add("learning_the_craft_first_mod", 		"  First Spell Modification", 					"test_badge", false);
		badgebook.Add("learning_the_craft_first_loop", 		"  First Loop Modification", 					"test_badge", false);
		badgebook.Add("learning_the_craft_first_if", 		"  First Conditional Modification", 			"test_badge", false);
		badgebook.Add("learning_the_craft_first_list", 		"  First List", 								"test_badge", false);
		*/
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
