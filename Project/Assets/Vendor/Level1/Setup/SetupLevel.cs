using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SetupLevel : MonoBehaviour {
	CodeScrollItem item;
	private bool crate1 = false;
	private bool crate2 = false;
	private bool hintstart = true;
	
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
		(new SetupSpellKiller()).Init();
		
		logStart();

		givePlayerASpellbook();
		givePlayerABadgeBook();
		givePlayerAFlag();
		//givePlayerAScroll();
		
		setupSpecialEvents();  //i.e. do random shit
	}
	
	void givePlayerAFlag() {
		// If all the bread is collected, give them a staff/flag
		FlyQuestChecker.UnlockedStaff += () => {
			GameObject game_flag = new GameObject();
			game_flag.AddComponent<Flag>();
			game_flag.name = "game_flag";
			
			Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
			inventory.addItem(game_flag);

			game_flag.GetComponent<Item>().item_name = "Staff";
		};
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
		
		spellbook.Add(new FilePage("MySpell", "MySpell/texture", "MySpell/code"));
		spellbook.Add(new FilePage("MassiveLevitation", "MassiveLevitation/texture", "MassiveLevitation/code"));
		spellbook.Add(new FilePage("FollowTheLeader", "FollowTheLeader/texture", "FollowTheLeader/code"));
		spellbook.Add(new FilePage("Flame", "Flame/texture", "Flame/code"));
		spellbook.Add(new FilePage("Sentry", "Sentry/texture", "Sentry/code"));
		spellbook.Add(new FilePage("Levitate", "Levitate/texture", "Levitate/code"));
		spellbook.Add(new FilePage("AdeptLevitate", "AdeptLevitate/texture", "AdeptLevitate/code"));
		spellbook.Add(new FilePage("Teleport", "Teleport/texture", "Teleport/code"));
		spellbook.Add(new FilePage("Flight", "Flight/texture", "Flight/code"));
		spellbook.Add(new FilePage("Summon", "Summon/texture", "Summon/code"));
		spellbook.Add(new FilePage("MassiveFire", "MassiveFire/texture", "MassiveFire/code"));
		spellbook.Add(new FilePage("Architecture", "Architecture/texture", "Architecture/code"));
		spellbook.Add(new FilePage("Architecture2", "Architecture2/texture", "Architecture2/code"));
	}
	
	void givePlayerAScroll()
	{
		CodeScrollItem item;

		GameObject initial_scroll = new GameObject();
		initial_scroll.name = "InitialScroll";
		initial_scroll.AddComponent<CodeScrollItem>();
		item = initial_scroll.GetComponent<CodeScrollItem>();
		item.item_name = "Blank";
		item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
		
		CodeScrollItem code_scroll_item_component = initial_scroll.GetComponent<CodeScrollItem>();
		code_scroll_item_component.setCurrentFile("Test.java");
		
		code_scroll_item_component.getIDEInput().SetCode("import june.*;public class Test extends Spell{public void cast(){Enchanted target = getTarget();target.onFire(true);}}");

		GameObject.Find("Inventory").GetComponent<Inventory>().addItem(initial_scroll);
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
		badgebook.Add("helping_others_cross_river", 	"  Cross River", 				"incomplete_crossing_river_badge", false);
		badgebook.Add("helping_others_reaching_up_high", "  New Heights", 				"incomplete_reaching_up_high_badge", false);
		badgebook.Add("helping_others_putting_something_high", 	"  Out of Reach", 		"incomplete_putting_something_high_badge", false);
		badgebook.Add("helping_others_putting_out_fire", 	"  Firefighter", 				"incomplete_putting_out_fire_badge", false);
		badgebook.Add("helping_others_light_fire", 		"  Light Fire", 				"incomplete_light_fire_badge", false);
		
		badgebook.Add("blank_line_hack1", 					"", 			"", false);
		badgebook.Add("blank_line_hack2", 					"", 			"", false);
		badgebook.Add("blank_line_hack3", 					"", 			"", false);

		badgebook.Add("reading_your_book", 					"READING YOUR BOOK", 			"incomplete_reading_your_book_badge", false);
		badgebook.Add("reading_your_book_fire", 			"  Flames", 					"incomplete_cast_flame_badge", false);
		badgebook.Add("reading_your_book_sentry", 			"  Sentry", 					"incomplete_cast_sentry_badge", false);
		badgebook.Add("reading_your_book_adv_levitate", 	"  Adept Levitation", 			"incomplete_cast_levitate_badge", false);
		badgebook.Add("reading_your_book_flight", 			"  Flight", 					"incomplete_cast_flight_badge", false);
		badgebook.Add("reading_your_book_teleport", 		"  Teleportation", 				"incomplete_cast_teleport_badge", false);
		badgebook.Add("reading_your_book_summon", 			"  Summoning", 					"incomplete_cast_summoning_badge", false);
		badgebook.Add("reading_your_book_massive", 			"  Massive Fire", 				"incomplete_cast_massive_fire_badge", false);
		badgebook.Add("reading_your_book_architecture", 	"  Architecture", 				"incomplete_cast_architecture_badge", false);
		badgebook.Add("collecting_objects_staff", 			"  ", 							"", false);
		
		//Set up the callbacks for unlocking the badges.
		int num_unlocked = 0;
		Enchantable.EnchantmentEnded += (spell_target, item_name) => {			
			bool success = false;
			if(item_name.StartsWith("Flame"))
				success =  badgebook.Complete("reading_your_book_fire");
			
			if(item_name.StartsWith("Sentry"))
				success =  badgebook.Complete("reading_your_book_sentry");
			
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
		
		int collectedBread = 0;
		int helpingUnlocked = 0;
		
		FlyQuestChecker.Levitated += () => {
			badgebook.Complete("helping_others_reaching_up_high");
			helpingUnlocked++;
		};
		
		
		AudioSource main_audio = GameObject.Find("Voice").audio;
			
		AudioClip pickup_clip = Resources.Load("PickupArpeg") as AudioClip;

		CollectBread.CollectedBread += () => {
			collectedBread++;
			
			main_audio.PlayOneShot(pickup_clip);
			Popup.mainPopup.popup("Got it! (" + (12 - collectedBread) + " left)");
			
			if(collectedBread == 12)
			{
				badgebook.Complete("helping_others_putting_something_high");	
				helpingUnlocked++;
			}
			
			if(helpingUnlocked == 6)
				badgebook.Complete("helping_others");
		};
		
		Inventory.PickedUp += (target) => {
			if(target.name.Equals("Presents"))
			{
				badgebook.Complete("helping_others_picking_up_item");	
				helpingUnlocked++;
			}
			if(target.name.Equals("Flag"))
			{
				badgebook.Complete("collecting_objects_staff");
				helpingUnlocked++;
			}
			if(helpingUnlocked == 6)
				badgebook.Complete("helping_others");
		};
		
		Inventory.DroppedOff += (target) => {
			GameObject gnome = GameObject.Find("QuestSwampGnomeEnd");
			GameObject presents = GameObject.Find("Presents");
						
			if(target.name.Equals("Presents") && Vector3.Distance(gnome.transform.position, presents.transform.position) < 10)
			{
				badgebook.Complete("helping_others_cross_river");
				helpingUnlocked++;
			}
			if(helpingUnlocked == 6)
				badgebook.Complete("helping_others");
		};
		
		Flamable.Extinguished += (target) => {
			//Debug.Log("Extinguished was called with target: "+target.gameObject.name);
			if(target.name.Equals("QuestSummonCrate"))
			{
				Debug.Log("Completing the summoning quest");
				

				badgebook.Complete("helping_others_putting_out_fire");	
				helpingUnlocked++;
			}
			if(helpingUnlocked == 6)
				badgebook.Complete("helping_others");
		};
		
		Flamable.CaughtFire += (target) => {
			if(!crate1 && target.name.Equals("QuestFlammingCrate"))
			{
				crate1 = true;
				if(crate2)
				{
					badgebook.Complete("helping_others_light_fire");	
					helpingUnlocked++;
				}
			}
			
			if(!crate2 && target.name.Equals("QuestFlammingCrate1"))
			{
				crate2 = true;
				if(crate1)
				{
					badgebook.Complete("helping_others_light_fire");
					helpingUnlocked++;
				}
			}
			if(helpingUnlocked == 6)
				badgebook.Complete("helping_others");
		};
	}
	
	public string getSpellName(string cont) {
		string title = "";
		string[] lines = cont.Split("\n"[0]);
		foreach (string ln in lines) {
			Match mat = (new Regex("class")).Match (ln);
			if (mat.Success) {
				title = ln.Substring(mat.Groups[0].Index+6);
				mat = (new Regex(" ")).Match (title);
				if (mat.Success) {
					title = title.Substring(0, mat.Groups[0].Index);
				}
				return title;
			}
		}
		return title;
	}

	void setupSpecialEvents() 
	{
		//Remove a spell from the inventory if the user blanks out the file contents.
		//  This is how we'll delete spells (for now).
		IDE.IDEClosed += (file_name, contents) => {
			
			//	path += segs[i];

			
			
			string newName = getSpellName(contents);

			string[] segs = file_name.Split('/');
			//if (newName.Equals("")) {
			//	return;
			//}

			string prevName = segs[segs.Length - 1].Replace(".java","");;
			
			
			
			Inventory i = GameObject.Find("Inventory").GetComponent<Inventory>();
			
			//previous name
			List<GameObject> matching_items = i.getMatching(prevName);
			
			if(Regex.Match(contents.Replace("\n",""), "^\\s*$").Success && matching_items.Count > 0)
			{
				Debug.Log ("About to remove item");
				i.removeItem(matching_items[0]);
				return;
			}
			if (!prevName.Equals(newName)) {
				//matching_items[0].GetComponent<Item>().item_name = newName;
				matching_items[0].GetComponent<CodeScrollItem>().setCurrentFile(newName+".java");
				matching_items[0].GetComponent<CodeScrollItem>().getIDEInput().SetCode(contents);
				ProgramLogger.LogKVtime("rename", prevName+", "+newName);
			}
			matching_items[0].GetComponent<CodeScrollItem>().SetCompilable();
		};
		
		
		AudioClip monster_clip = Resources.Load("Growls") as AudioClip;
		//Setup sounds
		Monster.AttackStarted += (monster) => {
			monster.audio.PlayOneShot(monster_clip);
			Popup.mainPopup.popup("Monster awoken!  Hide in the swamp!");
		};
		
		Monster.AttackEnded += (monster) => {
			monster.audio.PlayOneShot(monster_clip);
			Popup.mainPopup.popup("You lost him!");
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
		
		
		AudioClip drop_item_clip = Resources.Load("DropItem") as AudioClip;
		Inventory.DroppedOff += (target) => {
			main_audio.audio.PlayOneShot(drop_item_clip);	
		};
		
				
		AudioClip badge_clip = Resources.Load("BadgeUnlocked") as AudioClip;
		Badgebook.BadgeUnlocked += (target) => {
			main_audio.audio.PlayOneShot(badge_clip);	
		};
		
		ConversationDisplayer.ConversationStarted += (target) => {
			int i = Random.Range(1, 7);

			AudioClip hi_clip = Resources.Load("GnomeHi" + i) as AudioClip;

			main_audio.audio.PlayOneShot(hi_clip);	
		};
		
		ConversationDisplayer.ConversationStopped += (target) => {
			int i = Random.Range(1, 3);
			AudioClip bye_clip = Resources.Load("GnomeBye" + i) as AudioClip;

			main_audio.audio.PlayOneShot(bye_clip);	
		};
	}
	
	void logStart()
	{
	    TraceLogger.LogKV("session", "start");
	    ProgramLogger.LogKV("session", "start");
	    
	    // log all the gnomes you can talk to and their positions
	    NPCQuestTalk[] gnomes = Object.FindObjectsOfType(typeof(NPCQuestTalk)) as NPCQuestTalk[];
	    foreach (NPCQuestTalk gnome in gnomes) {
	        if (gnome.name.Contains("Gnome"))
	            TraceLogger.LogKV("gnome", gnome.name+", "+gnome.transform.position);
	    }
	}
	
	void OnApplicationQuit()
	{
	    TraceLogger.LogKVtime("session", "stop");
	    ProgramLogger.LogKVtime("session", "stop");
	}

	void OnGUI()
	{
	    if (GUI.Button(new Rect(Screen.width-30, Screen.height-30, 30, 30), "H")) {
	        TraceLogger.LogKVtime("hint", ""+hintstart);
	        ProgramLogger.LogKVtime("hint", ""+hintstart);
	        hintstart = !hintstart;
	    }
	}

}
