using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class SetupLevel : MonoBehaviour {
	CodeScrollItem item;
	private bool crate1 = false;
	private bool crate2 = false;
	private bool hintstart = false;
	private string currentQuest = "";
	
	private int helpingUnlocked = 0;
	private int num_unlocked = 0;
	private const int NUMBER_OF_QUESTS = 8 + 1; //extra 1 for staff, though it's not exactly a quest, it is used to unlock helping_others
	
	private GUIStyle helpButtonStyle = new GUIStyle();
	private GUIStyle currentQuestStyle = new GUIStyle();
	private GUIStyle currentQuestButtonStyle = new GUIStyle();
	private Texture2D yellowBorder;
	private bool showYellowBorder = false;
	
	public GameObject UnlevitationQuest;
	
	private Texture2D orangeBorder;
	
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
		givePlayerExistingSpells();

		setupSpecialEvents();  //i.e. do random shit
		
		// Setup help button
		createYellowBorderTexture();
		helpButtonStyle.normal.background = Resources.Load("Textures/red_button") as Texture2D;
		helpButtonStyle.active.background = Resources.Load("Textures/darker_red_button") as Texture2D;
		helpButtonStyle.normal.textColor = Color.yellow;
		helpButtonStyle.active.textColor = new Color(0.75f, 0.68f, 0.016f);
		helpButtonStyle.alignment = TextAnchor.MiddleCenter;
		helpButtonStyle.fontSize = 20;

		currentQuestStyle.normal.background = colorTexture(Color.yellow);
		currentQuestStyle.alignment = TextAnchor.MiddleCenter;

		currentQuestButtonStyle.normal.background = colorTexture(1, 180.0f/255.0f, 40.0f/255.0f);
		currentQuestButtonStyle.active.background = colorTexture(Color.yellow);
		currentQuestButtonStyle.alignment = TextAnchor.MiddleCenter;

		// Setup quest check border
		createOrangeBorderTexture();
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
		//spellbook.Add(new FilePage("MassiveLevitation", "MassiveLevitation/texture", "MassiveLevitation/code"));
		//spellbook.Add(new FilePage("FollowTheLeader", "FollowTheLeader/texture", "FollowTheLeader/code"));
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
	    
	    badgebook.AddColumn(NUMBER_OF_QUESTS+1);
		badgebook.Add("helping_others", 						"HELPING OTHERS", 		"incomplete_helping_others_badge", false);
		badgebook.Add("helping_others_picking_up_item",			"  Pickin up Stuff", 	"incomplete_picking_up_item_badge", false);
		badgebook.Add("helping_others_cross_river", 			"  Cross River", 		"incomplete_crossing_river_badge", false);
		badgebook.Add("helping_others_reaching_up_high", 		"  New Heights", 		"incomplete_reaching_up_high_badge", false);
		badgebook.Add("helping_others_putting_something_high", 	"  Out of Reach", 		"incomplete_putting_something_high_badge", false);
		badgebook.Add("helping_others_putting_out_fire", 		"  Firefighter", 		"incomplete_putting_out_fire_badge", false);
		badgebook.Add("helping_others_light_fire", 				"  Light Fire", 		"incomplete_light_fire_badge", false);
		badgebook.Add("helping_others_unlevitate",				"  Unlevitate",			"incomplete_unlevitate_badge", false);
		badgebook.Add("helping_others_summonObject",			"  Summon Object",		"incomplete_summonObject_badge", false);
		badgebook.Add("collecting_objects_staff", 			    "  Staff", 				"incomplete_collecting_objects_staff", false);

        badgebook.AddColumn(9);
		badgebook.Add("reading_your_book", 					"READING YOUR BOOK", 			"incomplete_reading_your_book_badge", false);
		badgebook.Add("reading_your_book_fire", 			"  Flames", 					"incomplete_cast_flame_badge", false);
		badgebook.Add("reading_your_book_sentry", 			"  Sentry", 					"incomplete_cast_sentry_badge", false);
		badgebook.Add("reading_your_book_adv_levitate", 	"  Adept Levitation", 			"incomplete_cast_levitate_badge", false);
		badgebook.Add("reading_your_book_flight", 			"  Flight", 					"incomplete_cast_flight_badge", false);
		badgebook.Add("reading_your_book_teleport", 		"  Teleportation", 				"incomplete_cast_teleport_badge", false);
		badgebook.Add("reading_your_book_summon", 			"  Summoning", 					"incomplete_cast_summoning_badge", false);
		badgebook.Add("reading_your_book_massive", 			"  Massive Fire", 				"incomplete_cast_massive_fire_badge", false);
		badgebook.Add("reading_your_book_architecture", 	"  Architecture", 				"incomplete_cast_architecture_badge", false);
		
		badgebook.AddColumn(8);
		badgebook.Add("square_dance", "  Square Dance", "incomplete_square_dance", false);
		badgebook.MakeButtonUnlockable("square_dance");
		badgebook.Add("creative_dance", "  Creative Dance", "incomplete_creative_dance", false);
		badgebook.MakeButtonUnlockable("creative_dance");
		badgebook.Add("massive_levitate", "  Massive Levitate", "incomplete_massive_levitate", false);
		badgebook.MakeButtonUnlockable("massive_levitate");
		badgebook.Add("massive_unlevitate", "  Massive Unlevitate", "incomplete_massive_unlevitate", false);
		badgebook.MakeButtonUnlockable("massive_unlevitate");
		badgebook.Add("massive_dance", "  Massive Dance", "incomplete_massive_dance", false);
		badgebook.MakeButtonUnlockable("massive_dance");
		badgebook.Add("follow_the_leader", "  Follow The Leader", "incomplete_follow_the_leader", false);
		badgebook.MakeButtonUnlockable("follow_the_leader");
		badgebook.Add("portal", "  Portal", "incomplete_portal", false);
		badgebook.MakeButtonUnlockable("portal");
		badgebook.Add("umbrella", "  Umbrella", "incomplete_umbrella", false);
		badgebook.MakeButtonUnlockable("umbrella");
		
		// To make a badge unlockable by an instructor, add
		// badgebook.MakeButtonUnlockable(<badge_name>);
		// example:
		// badgebook.MakeButtonUnlockable("reading_your_book");
		
		// mark badges as already complete
		markCompletedBadges();
		
		showAppropriateQuestArrows();
		
		//Set up the callbacks for unlocking the badges.
		//int num_unlocked = 0;
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
			
			if(num_unlocked == 8)
			{
				badgebook.Complete("reading_your_book");
			}
			
			showAppropriateQuestArrows();
		};
		
		int collectedBread = 0;

		Badgebook.BadgeSelected += (badge) => {
			currentQuest = badge.name;
			showAppropriateQuestArrows();
		};
		
		FlyQuestChecker.Levitated += () => {
			if (badgebook.Complete("helping_others_reaching_up_high"))
			{
				//GameObject.Find("LevitationQuestGnomeArmature").GetComponent<DisplayOnMinimap>().show = false;
				//GameObject.Find("LevitationQuestGnomeArmature").GetComponent<Icon3D>().show = false;
			    helpingUnlocked++;
			}
			
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		AudioSource main_audio = GameObject.Find("Voice").audio;
			
		AudioClip pickup_clip = Resources.Load("PickupArpeg") as AudioClip;
		
		int unlevitatingBoxes = 0;
		Unlevitate.UnlevitateQuest += () => {
			unlevitatingBoxes++;
			main_audio.PlayOneShot(pickup_clip);
			Popup.mainPopup.popup("Got it! (" + (3 - unlevitatingBoxes) + " left)");
			
			if(unlevitatingBoxes == 3)
			{
				if (badgebook.Complete("helping_others_unlevitate"))
				{
					//GameObject.Find("UnlevitationQuestGnomeArmature").GetComponent<DisplayOnMinimap>().show = false;
					//GameObject.Find("UnlevitationQuestGnomeArmature").GetComponent<Icon3D>().show = false;
					//GameObject.Find("QuestUnlevitationCrate").GetComponent<Icon3D>().show = false;
					//GameObject.Find("QuestUnlevitationCrate1").GetComponent<Icon3D>().show = false;
					//GameObject.Find("QuestUnlevitationCrate2").GetComponent<Icon3D>().show = false;
				    helpingUnlocked++;
				}
			}
			
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		SummonObject.SummonObjectQuest += () => {
			if (badgebook.Complete("helping_others_summonObject"))
			{
				//GameObject.Find("SummonObjectGnomeArmature").GetComponent<DisplayOnMinimap>().show = false;
				//GameObject.Find("SummonObjectGnomeArmature").GetComponent<Icon3D>().show = false;
				//GameObject.Find("SummonedObject").GetComponent<Icon3D>().show = false;
				
				//Destroy the yellow summoning block when they have summond the square to it.
				Destroy(GameObject.Find("SummonTo"));
				helpingUnlocked++;
			}
			
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		CollectBread.CollectedBread += () => {
			collectedBread++;
			
			main_audio.PlayOneShot(pickup_clip);
			Popup.mainPopup.popup("Got it! (" + (12 - collectedBread) + " left)");
			
			if(collectedBread == 12)
			{
				if (badgebook.Complete("helping_others_putting_something_high"))
				{
					//GameObject.Find("FlyQuestGnomeArmature").GetComponent<DisplayOnMinimap>().show = false;
					//GameObject.Find("FlyQuestGnomeArmature").GetComponent<Icon3D>().show = false;
				    helpingUnlocked++;
				}
			}
			
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		Inventory.PickedUp += (target) => {
			if(target.name.Equals("Presents"))
			{
				//Debug.Log("Trying to enable the Icon3D for Dance Quest");
				//GameObject.Find("DanceQuest").GetComponent<Icon3D>().color = Color.white;
				
				if (badgebook.Complete("helping_others_picking_up_item"))	
				    helpingUnlocked++;
			}
			if(target.name.Equals("game_flag"))
			{
				if (badgebook.Complete("collecting_objects_staff"))
				    helpingUnlocked++;
			}
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		Inventory.DroppedOff += (target) => {
			GameObject gnome = GameObject.Find("QuestSwampGnomeEnd");
			GameObject presents = GameObject.Find("Presents");
						
			if(target.name.Equals("Presents") && Vector3.Distance(gnome.transform.position, presents.transform.position) < 10)
			{
				//GameObject.Find("QuestSwampGnomeEnd").GetComponent<Icon3D>().enabled = false;
				if (badgebook.Complete("helping_others_cross_river"))
				    helpingUnlocked++;
			}
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		Flamable.Extinguished += (target) => {
			//Debug.Log("Extinguished was called with target: "+target.gameObject.name);
			if(target.name.Equals("QuestSummonCrate"))
			{
				Debug.Log("Completing the summoning quest");
				

				if (badgebook.Complete("helping_others_putting_out_fire"))
				    helpingUnlocked++;
			}
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
		
		Flamable.CaughtFire += (target) => {
			if(!crate1 && target.name.Equals("QuestFlammingCrate"))
			{
				crate1 = true;
				if(crate2)
				{
					if (badgebook.Complete("helping_others_light_fire"))
					    helpingUnlocked++;
				}
			}
			
			if(!crate2 && target.name.Equals("QuestFlammingCrate1"))
			{
				crate2 = true;
				if(crate1)
				{
					if (badgebook.Complete("helping_others_light_fire"))
					    helpingUnlocked++;
				}
			}
			if(helpingUnlocked == NUMBER_OF_QUESTS)
				badgebook.Complete("helping_others");
			
			showAppropriateQuestArrows();
		};
	}
	
	void givePlayerExistingSpells() {
// 	    System.Diagnostics.Process reader = Shell.shell_no_start("./buildSpellsLog.py", "CodeSpellsProgram.log CodeSpellsSpells.log");
// 	    reader.Start();
// 	    reader.WaitForExit();
// 	    reader.Close();
        Spellbook spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
	    Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
	    if (SpellLogger.loggedSpells.Length > 0) {
            string[] parts, keyed;
            foreach (string line in SpellLogger.loggedSpells) {
                keyed = line.Split(new char[] {':'});
                if (keyed.Length == 1) {    //support for old spell log formats
                    parts = line.Split(new char[] {','});
                    if (parts.Length == 1)
                        spellbook.addExistingSpell(line, "");
                    else {
                        byte[] bs = System.Convert.FromBase64String(parts[1].Trim());
                        string code = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length);
                        spellbook.addExistingSpell(parts[0].Trim(), code);
                    }
                } else {
                    parts = keyed[1].Split(new char[] {','});
                    if (keyed[0] == "code") {
                        byte[] bs = System.Convert.FromBase64String(parts[1].Trim());
                        string code = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length);
                        spellbook.addExistingSpell(parts[0].Trim(), code);
                    } else if (keyed[0] == "rename") {
                        string old = parts[0].Trim();
                        string newn = parts[1].Trim();
                        //this could perhaps instead be accomplished with File.Move(...)
//                         List<GameObject> matching_items = inventory.getMatching(old);
//                         if (matching_items.Count != 1) {
//                             Debug.Log("matching_items length "+matching_items.Count);
//                             continue;
//                         }
                        CodeScrollItem item = inventory.getCodeScrollItem(old).GetComponent<CodeScrollItem>();
                        //CodeScrollItem item = matching_items[0].GetComponent<CodeScrollItem>();
//                         if (item == null) {
//                             Debug.Log("Renaming CodeScrollItem null");
//                             continue;
//                         } else
//                             Debug.Log("Renaming CodeScrollItem "+item.getName());
                        string code = item.getIDEInput().GetCode();
                        item.setCurrentFile(newn+".java");
				        item.getIDEInput().SetCode(code);
				        spellbook.setNameCounter(newn);
                    } else if (keyed[0] == "delete") {
                        List<GameObject> matching_items = inventory.getMatching(parts[0].Trim());
                        if (matching_items.Count > 0) {
                            Debug.Log("matching items: "+matching_items.Count);
                            inventory.removeItem(matching_items[0]);
                        }
                    }
                }
	        }
	        inventory.clearRemovedItems();
	        foreach (CodeScrollItem item in inventory.getAllCodeScrollItems()) {
	            string code = item.getIDEInput().GetCode();
	            SpellLogger.LogCode(item.getName(), code);
	        }
	    }
// 	    if (File.Exists("./CodeSpellsSpells.log")) {
//             string[] lines = File.ReadAllLines("./CodeSpellsSpells.log");
//             Spellbook spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
//             string[] parts;
//             foreach (string line in lines) {
//                 parts = line.Split(new char[] {','});
//                 if (parts.Length == 1)
// 	                spellbook.addExistingSpell(line, "");
// 	            else {
// 	                byte[] bs = System.Convert.FromBase64String(parts[1].Trim());
// 	                string code = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length);
// 	                spellbook.addExistingSpell(parts[0].Trim(), code);
// 	            }
// 	        }
// 	    }
	}
	
	void showAppropriateQuestArrows() {
		hideAllQuestArrows();

		if (currentQuest != "") {
			//Debug.Log("showing quest arrows for " + currentQuest);
			showQuestObjects(objectNameForQuest(currentQuest));
		} else {
			foreach (string questName in nextQuests()) {
				//Debug.Log("showing quest arrows for " + questName);
				showQuestObjects(objectNameForQuest(questName));
			}
		}
	}
	
	void hideAllQuestArrows() {
		foreach (Icon3D arrow in GameObject.Find("IntroGnome").GetComponentsInChildren<Icon3D>(true)) {
			arrow.show = false;
		}
		foreach (DisplayOnMinimap arrow in GameObject.Find("IntroGnome").GetComponentsInChildren<DisplayOnMinimap>(true)) {
			arrow.show = false;
		}
		foreach (Icon3D arrow in GameObject.Find("Quests").GetComponentsInChildren<Icon3D>(true)) {
			arrow.show = false;
		}
		foreach (DisplayOnMinimap arrow in GameObject.Find("Quests").GetComponentsInChildren<DisplayOnMinimap>(true)) {
			arrow.show = false;
		}
	}
	
	void showQuestObjects(string objectName) {
	    if (objectName == "")
	        return;
		foreach (Icon3D arrow in GameObject.Find(objectName).GetComponentsInChildren<Icon3D>(true)) {
			arrow.show = true;
		}
		foreach (DisplayOnMinimap arrow in GameObject.Find(objectName).GetComponentsInChildren<DisplayOnMinimap>(true)) {
			arrow.show = true;
		}
	}
	
	string[] nextQuests() {
		Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
		
		if (!badgebook.IsComplete("helping_others_picking_up_item")) return new string[] {"intro", "helping_others_picking_up_item"};
		if (!badgebook.IsComplete("helping_others_cross_river")) return new string[] {"helping_others_cross_river"};
		if (!badgebook.IsComplete("helping_others_reaching_up_high")) return new string[] {"helping_others_reaching_up_high"};
		if (!badgebook.IsComplete("helping_others_putting_out_fire")) return new string[] {"helping_others_putting_out_fire"};
		if (!badgebook.IsComplete("helping_others_light_fire")) return new string[] {"helping_others_light_fire"};
		if (!badgebook.IsComplete("helping_others_summonObject")) return new string[] {"helping_others_summonObject"};
		if (!badgebook.IsComplete("helping_others_unlevitate")) return new string[] {"helping_others_unlevitate"};
		if (!badgebook.IsComplete("square_dance")) return new string[] {"square_dance"};
		if (!badgebook.IsComplete("creative_dance")) return new string[] {"creative_dance"};
		if (!badgebook.IsComplete("massive_levitate")) return new string[] {"massive_levitate"};
		if (!badgebook.IsComplete("massive_unlevitate")) return new string[] {"massive_unlevitate"};
		if (!badgebook.IsComplete("massive_dance")) return new string[] {"massive_dance"};
		if (!badgebook.IsComplete("follow_the_leader")) return new string[] {"follow_the_leader"};
		if (!badgebook.IsComplete("portal")) return new string[] {"portal"};
		if (!badgebook.IsComplete("umbrella")) return new string[] {"umbrella"};

		return new string[] {};
	}
	
	string objectNameForQuest(string questName) {
		switch (questName) {
		case "intro": return "IntroGnome";
		case "helping_others_picking_up_item": return "RiverQuest";
		case "helping_others_cross_river": return "RiverQuest";
		case "helping_others_reaching_up_high": return "FlyingQuest";
		case "helping_others_putting_out_fire": return "SummonQuest";
		//case "light fire": return "SummonQuest";
		case "helping_others_light_fire": return "FireQuest";
		case "helping_others_summonObject": return "SummonObjectQuest";
		case "helping_others_unlevitate": return "UnlevitationQuest";
		case "square_dance": return "DanceQuest";
		case "creative_dance": return "DanceQuest";
		case "massive_levitate": return "MassiveLevitationQuest";
		case "massive_unlevitate": return "MassiveLevitationQuest";
		//case "massive creative dance": return "";
		case "follow_the_leader": return "FollowTheLeaderQuest";
		case "portal": return "PortalQuest";
		case "umbrella": return "UmbrellaQuest";
		}
		return "";
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
			string newName = getSpellName(contents);
			string[] segs = file_name.Split('/');
			string prevName = segs[segs.Length - 1].Replace(".java","");
			
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
			    // this spell already exists in the inventory so we need to automatically rename it
                Spellbook spellbook = GameObject.Find("Spellbook").GetComponent<Spellbook>();
                string newNameAdjusted = spellbook.getIncName(newName);
                contents = contents.Replace(newName, newNameAdjusted);
                newName = newNameAdjusted;
				//matching_items[0].GetComponent<Item>().item_name = newName;
				matching_items[0].GetComponent<CodeScrollItem>().setCurrentFile(newName+".java");
				matching_items[0].GetComponent<CodeScrollItem>().getIDEInput().SetCode(contents);
				ProgramLogger.LogKVtime("rename", prevName+", "+newName);
				SpellLogger.Log("rename: "+prevName+", "+newName);
			}
			matching_items[0].GetComponent<CodeScrollItem>().SetCompilable();
			ProgramLogger.LogKV("compilable", newName+", "+matching_items[0].GetComponent<CodeScrollItem>().IsCompilable());
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
		
		ConversationDisplayer.ConversationStopped += (target, startingQuest) => {
			int i = Random.Range(1, 3);
			AudioClip bye_clip = Resources.Load("GnomeBye" + i) as AudioClip;

			main_audio.audio.PlayOneShot(bye_clip);

			if (startingQuest != "") {
				Debug.Log("Quest accepted: " + startingQuest);
				currentQuest = startingQuest;
				showAppropriateQuestArrows();
			}
		};
	}
	
	// Reads a log of completed badges and marks them as already complete
	void markCompletedBadges() {
	    if (File.Exists("./CodeSpellsBadges.log")) {
            string[] lines = File.ReadAllLines("./CodeSpellsBadges.log");
            Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
            foreach (string line in lines) {
                if (badgebook.MarkAlreadyComplete(line.Trim())) {
                    if (line.StartsWith("helping_others_"))
					{
						helpingUnlocked++;
					}
                        
                    else if (line.StartsWith("reading_your_book_"))
                        num_unlocked++;
                    else if (line.StartsWith("collecting_objects_staff")) {
                        helpingUnlocked++;
                        GameObject game_flag = new GameObject();
                        game_flag.AddComponent<Flag>();
                        game_flag.name = "game_flag";
            
                        Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
                        inventory.addItem(game_flag);

                        game_flag.GetComponent<Item>().item_name = "Staff";
                    }
                }
            }
	    }
	}
	
	void logStart()
	{
	    TraceLogger.LogStart();
	    ProgramLogger.LogStart();
	    
	    // log all the gnomes you can talk to and their positions
	    NPCQuestTalk[] gnomes = Object.FindObjectsOfType(typeof(NPCQuestTalk)) as NPCQuestTalk[];
	    foreach (NPCQuestTalk gnome in gnomes) {
	        if (gnome.name.Contains("Gnome"))
	            TraceLogger.LogKV("gnome", gnome.name+", "+gnome.transform.position);
	    }
	}
	
	void OnApplicationQuit()
	{
	    TraceLogger.LogStop();
	    ProgramLogger.LogStop();
	}

	void OnGUI()
	{
	    string redButtonText = "Help";
	    Badgebook badgebook = GameObject.Find("Badgebook").GetComponent<Badgebook>();
	    // If quest check has been requested, display an orange border
	    if (badgebook.showOrangeBorder) {
	        int thick = 10;
	        GUI.DrawTexture(new Rect(0, 0, Screen.width, thick), orangeBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(0, 0, thick, Screen.height), orangeBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(Screen.width-thick, 0, thick, Screen.height), orangeBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(0, Screen.height-thick, Screen.width, thick), orangeBorder, ScaleMode.StretchToFill, false);
	        if (showYellowBorder) {
                redButtonText = "Ok";
                GUI.DrawTexture(new Rect(thick, thick, Screen.width-2*thick, thick), yellowBorder, ScaleMode.StretchToFill, false);
                GUI.DrawTexture(new Rect(thick, thick, thick, Screen.height-2*thick), yellowBorder, ScaleMode.StretchToFill, false);
                GUI.DrawTexture(new Rect(Screen.width-2*thick, thick, thick, Screen.height-2*thick), yellowBorder, ScaleMode.StretchToFill, false);
                GUI.DrawTexture(new Rect(thick, Screen.height-2*thick, Screen.width-2*thick, thick), yellowBorder, ScaleMode.StretchToFill, false);
            }
	    }
	    // If help has been requested, display a yellow border
	    else if (showYellowBorder) {
	        redButtonText = "Ok";
	        int thick = 10;
	        GUI.DrawTexture(new Rect(0, 0, Screen.width, thick), yellowBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(0, 0, thick, Screen.height), yellowBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(Screen.width-thick, 0, thick, Screen.height), yellowBorder, ScaleMode.StretchToFill, false);
	        GUI.DrawTexture(new Rect(0, Screen.height-thick, Screen.width, thick), yellowBorder, ScaleMode.StretchToFill, false);
	    }
	    bool oldhint = hintstart;
	    // Toggle control for marking start/end of giving a hint
	    hintstart = GUI.Toggle(new Rect(Screen.width-30, Screen.height-30, 30, 30), hintstart, "H");
	    if (oldhint != hintstart) {
	        showYellowBorder = false;
            TraceLogger.LogKVtime("hint", ""+hintstart);
            ProgramLogger.LogKVtime("hint", ""+hintstart);
        }
        // Display a button that requests help or cancels a call for help
        if (GUI.Button(new Rect(0, Screen.height-64, 64, 64), redButtonText+"!", helpButtonStyle)) {
            TraceLogger.LogKVtime("hint", redButtonText);
            ProgramLogger.LogKVtime("hint", redButtonText);
            showYellowBorder = !showYellowBorder;
        }

		if (currentQuest != "") {
			GUI.Label(new Rect(50, 10, Screen.width - 400, 20), "Current Quest: " + badgebook.badgeStore.label(currentQuest).Trim(), currentQuestStyle);
			int buttonWidth = 90;
			if (GUI.Button(new Rect(Screen.width - 400 + 50 - (buttonWidth + 2), 12, buttonWidth, 16), "Abandon!", currentQuestButtonStyle)) {
				currentQuest = "";
			}
		}
	}

	// Yellow border shows up around screen if help is requested
	void createYellowBorderTexture() {
	    yellowBorder = new Texture2D(1,1);
	    yellowBorder.SetPixel(0, 0, Color.yellow);
	    yellowBorder.Apply();
	}
	
	// Orange border shows up around screen if quest check is requested
	void createOrangeBorderTexture() {
	    orangeBorder = new Texture2D(1,1);
	    orangeBorder.SetPixel(0, 0, new Color(1,.5f,0,1));
	    orangeBorder.Apply();
	}
	
	Texture2D colorTexture(float r, float g, float b) {
		return colorTexture(new Color(r, g, b));
	}
	
	Texture2D colorTexture(Color color) {
		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, color);
		tex.Apply();
		return tex;
	}
}
