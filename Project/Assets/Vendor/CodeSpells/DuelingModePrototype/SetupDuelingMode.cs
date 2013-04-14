using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SetupDuelingMode : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
		 (new SetupInventory()).Init();		
		(new SetupUnidee()).Init();		
		(new SetupPopup()).Init();		
		(new SetupJune()).Init();		
		(new SetupHighlighter()).Init();
		(new SetupSpellbook()).Init();
		(new SetupBadgebook()).Init();
		(new SetupSpellKiller()).Init();
		//GUI.DrawTexture(new Rect(0,0,500,500),health_bar_background);
		
		
		givePlayerASpellbook();
		givePlayerABadgeBook();
		givePlayerAFlag();
		setupSpecialEvents();
		
		
		//load inventory_area_background
		
		
		
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
		spellbook.Add(new FilePage("Flame", "Flame/texture", "Flame/code"));
		spellbook.Add(new FilePage("Sentry", "Sentry/texture", "Sentry/code"));
		spellbook.Add(new FilePage("AdeptLevitate", "AdeptLevitate/texture", "AdeptLevitate/code"));
		spellbook.Add(new FilePage("Teleport", "Teleport/texture", "Teleport/code"));
		spellbook.Add(new FilePage("Flight", "Flight/texture", "Flight/code"));
		spellbook.Add(new FilePage("Summon", "Summon/texture", "Summon/code"));
		spellbook.Add(new FilePage("MassiveFire", "MassiveFire/texture", "MassiveFire/code"));
		spellbook.Add(new FilePage("Architecture", "Architecture/texture", "Architecture/code"));
		spellbook.Add(new FilePage("Architecture2", "Architecture2/texture", "Architecture2/code"));
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
	
		/*
		badgebook.Add("helping_others", 				"Example Badge", 				"incomplete_helping_others_badge", false);
		
		bool badge_given = false;
		Enchantable.EnchantmentEnded += (spell_target, item_name) => {			
			if(!badge_given)
				badgebook.Complete("helping_others");
			
			badge_given = true;
		};
		
		*/
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
			}
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
		
		//AudioSource main_audio = GameObject.Find("Voice").audio;
			
		AudioClip spellbook_clip = Resources.Load("PageTurn") as AudioClip;
		Spellbook.PageTurnedForward += (page) => {
			//main_audio.audio.PlayOneShot(spellbook_clip);
		};
		Spellbook.PageTurnedBackward += (page) => {
			//main_audio.audio.PlayOneShot(spellbook_clip);
		};
		Spellbook.SpellCopied += (page) => {
			//main_audio.audio.PlayOneShot(spellbook_clip);
		};
		
		
		AudioClip drop_item_clip = Resources.Load("DropItem") as AudioClip;
		Inventory.DroppedOff += (target) => {
			//main_audio.audio.PlayOneShot(drop_item_clip);	
		};
		
				
		AudioClip badge_clip = Resources.Load("BadgeUnlocked") as AudioClip;
		Badgebook.BadgeUnlocked += (target) => {
			//main_audio.audio.PlayOneShot(badge_clip);	
		};
		
		ConversationDisplayer.ConversationStarted += (target) => {
			int i = Random.Range(1, 7);

			AudioClip hi_clip = Resources.Load("GnomeHi" + i) as AudioClip;

			//main_audio.audio.PlayOneShot(hi_clip);	
		};
		
		ConversationDisplayer.ConversationStopped += (target) => {
			int i = Random.Range(1, 3);
			AudioClip bye_clip = Resources.Load("GnomeBye" + i) as AudioClip;

			//main_audio.audio.PlayOneShot(bye_clip);	
		};
	}
		
	
	void givePlayerAFlag() {
		// If all the bread is collected, give them a staff/flag
	//	FlyQuestChecker.UnlockedStaff += () => {
			GameObject game_flag = new GameObject();
			game_flag.AddComponent<Flag>();
			game_flag.name = "game_flag";
			
			Inventory inventory = GameObject.Find("Inventory").GetComponent(typeof(Inventory)) as Inventory;
			inventory.addItem(game_flag);
			
			game_flag.GetComponent<Item>().item_name = "Staff";
	//	};
	}
}
