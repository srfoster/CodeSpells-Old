using UnityEngine;
using System.Collections;

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
		
		givePlayerASpellbook();
		givePlayerABadgeBook();
		givePlayerAFlag();
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
		
		
		/*
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MySpell");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flame");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Sentry");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Levitate");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/AdeptLevitate");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Teleport");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Flight");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Summon");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/MassiveFire");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Architecture");
		spellbook.page_urls.Add("http://cseweb.ucsd.edu/~srfoster/code_spells/Architecture2");
		*/
		
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
