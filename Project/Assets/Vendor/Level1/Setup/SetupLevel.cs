using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class SetupLevel : MonoBehaviour {
	CodeScrollItem item;
	private bool crate1 = false;
	private bool crate2 = false;
	
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

		
		//ObjectManager.Register(GameObject.Find("First Person Controller"), "Me");
		
		givePlayerASpellbook();
		givePlayerABadgeBook();
		givePlayerAFlag();

		
		setupSpecialEvents();  //i.e. do random shit
		
	}
	
	
	void givePlayerAFlag() {
		GameObject game_flag = new GameObject();
		game_flag.AddComponent<Flag>();
		//GameObject game_flag = Instantiate(Resources.Load("Flag") as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
		game_flag.name = "game_flag";
		
		Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
		inventory.addItem(game_flag);
		
		game_flag.GetComponent<Item>().item_name = "Flag";
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
		
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MySpell");

		
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flame");

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Levitate");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/AdeptLevitate");
		
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Sentry");

		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Teleport");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flight");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Summon");
		
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MassiveFire");

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
//		badgebook.Add("helping_others_moving_rocks", 	"  Moving Cargo", 				"incomplete_moving_rocks_badge", false);
		
		badgebook.Add("blank_line_hack1", 					"", 			"", false);
		badgebook.Add("blank_line_hack2", 					"", 			"", false);
		badgebook.Add("blank_line_hack3", 					"", 			"", false);


		
		badgebook.Add("reading_your_book", 					"READING YOUR BOOK", 			"incomplete_reading_your_book_badge", false);
		badgebook.Add("reading_your_book_fire", 			"  Flames", 					"incomplete_cast_flame_badge", false);
		badgebook.Add("reading_your_book_adv_levitate", 	"  Adept Levitation", 			"incomplete_cast_levitate_badge", false);
		badgebook.Add("reading_your_book_flight", 			"  Flight", 					"incomplete_cast_flight_badge", false);
		badgebook.Add("reading_your_book_teleport", 		"  Teleportation", 				"incomplete_cast_teleport_badge", false);
		badgebook.Add("reading_your_book_summon", 			"  Summoning", 					"incomplete_cast_summoning_badge", false);
		badgebook.Add("reading_your_book_massive", 			"  Massive Fire", 				"incomplete_cast_massive_fire_badge", false);
		badgebook.Add("reading_your_book_architecture", 	"  Architecture", 				"incomplete_cast_architecture_badge", false);
		
		//Set up the callbacks for unlocking the badges.
		
		int num_unlocked = 0;
		Enchantable.EnchantmentEnded += (spell_target, item_name) => {
		
			bool success = false;
			if(item_name.StartsWith("Flame"))
				success =  badgebook.Complete("reading_your_book_fire");
			
			if(item_name.StartsWith("Massive"))
				success = badgebook.Complete("reading_your_book_massive");
			
			if(item_name.StartsWith("Flight"))
				success = badgebook.Complete("reading_your_book_flight");
			
			if(item_name.StartsWith("AdeptLevitate"))
				success = badgebook.Complete("reading_your_book_adv_levitate");
			
			if(item_name.StartsWith("Summon"))
				success = badgebook.Complete("reading_your_book_summon");
					
			if(item_name.StartsWith("Teleport"))
				success = badgebook.Complete("reading_your_book_teleport");
			
			if(item_name.StartsWith("Architecture"))
				success = badgebook.Complete("reading_your_book_architecture");
			
			if(success)
				num_unlocked++;
			
			if(num_unlocked == 7)
			{
				badgebook.Complete("reading_your_book");
			}
		};
		
		Inventory.PickedUp += (target) => {
			if(target.name.Equals("Presents"))
				badgebook.Complete("helping_others_picking_up_item");	
		};
		
		Inventory.DroppedOff += (target) => {
			GameObject gnome = GameObject.Find("QuestSwampGnome");
			GameObject presents = GameObject.Find("Presents");
			Debug.Log("The distance is: "+Vector3.Distance(gnome.transform.position, presents.transform.position));
			if(target.name.Equals("Presents") && Vector3.Distance(gnome.transform.position, presents.transform.position) < 10)
				badgebook.Complete("helping_others_cross_river");
		};
		
		Flamable.Extinguished += (target) => {
			if(target.name.Equals("QuestSummonCrate"))
			{
				badgebook.Complete("helping_others_put_out_fire");	
			}
		};
		
		Flamable.CaughtFire += (target) => {
			if(!crate1 && target.name.Equals("QuestFlammingCrate"))
			{
				crate1 = true;
				if(crate2)
					badgebook.Complete("helping_others_light_fire");	
			}
			
			if(!crate2 && target.name.Equals("QuestFlammingCrate1"))
			{
				crate2 = true;
				if(crate1)
					badgebook.Complete("helping_others_light_fire");
			}
		};
	}

	
	void setupSpecialEvents() // random shit
	{
		
		
		
		//Remove a spell from the inventory if the user blanks out the file contents.
		//  This is how we'll delete spells (for now).
		IDE.IDEClosed += (file_name, contents) => {
			string[] segs = file_name.Split('/');
			string short_name = segs[segs.Length - 1].Replace(".java","");
			
			Debug.Log(short_name + " : " + contents);
			
			Inventory i = GameObject.Find("Inventory").GetComponent<Inventory>();
			
			List<GameObject> matching_items = i.getMatching(short_name);
			
			if(Regex.Match(contents.Replace("\n",""), "^\\s*$").Success && matching_items.Count > 0)
			{
				i.removeItem(matching_items[0]);
			}
		};	
		
		
		
		AudioClip monster_clip = Resources.Load("Growls") as AudioClip;
		//Setup sounds
		Monster.AttackStarted += (monster) => {
			monster.audio.PlayOneShot(monster_clip);
		};
		
		
		AudioSource main_audio = GameObject.Find("Voice").audio;
			
		AudioClip spellbook_clip = Resources.Load("PageTurn") as AudioClip;
		Spellbook.PageTurnedForward += (page) => {
			main_audio.audio.PlayOneShot(spellbook_clip);
		};
		Spellbook.PageTurnedBackward += (page) => {
			main_audio.audio.PlayOneShot(spellbook_clip);
		};
		Spellbook.SpellCopied += (page) => {
			main_audio.audio.PlayOneShot(spellbook_clip);
		};
	}

}
