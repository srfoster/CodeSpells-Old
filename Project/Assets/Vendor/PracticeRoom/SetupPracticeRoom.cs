using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class SetupPracticeRoom : MonoBehaviour {
	public GameObject startCrate;
	public Camera firstPersonCamera;
	public Camera thirdPersonCamera;
	
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
		//(new SetupJune()).Init();			
		(new SetupHighlighter()).Init();
		(new SetupPracticeRoomSpellbook()).Init();
		(new SetupObstacles()).Init(startCrate);
		
		givePlayerASpellbook();
		givePlayerASolutionScroll();
		setupSpecialEvents();
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 55, 200, 40), "Stop Practicing"))
		{
			Application.LoadLevel(0);	
		}
	}
	
	void givePlayerASolutionScroll()
	{
		Debug.Log ("Trying to give player a scroll, creating a scroll gameobject");
		//Creates the actual scroll
		GameObject solution_scroll = new GameObject();
		solution_scroll.name = "Solution";
		
		Debug.Log ("Adding a codescrollitem");
		//Give the gameobject a CodeScrollItem
		solution_scroll.AddComponent<CodeScrollItem>();
		
		Debug.Log ("Setting fields of code scroll item");
		//Getting the CodeScrollItem so that we can set the file and texture
		CodeScrollItem item = solution_scroll.GetComponent<CodeScrollItem>();
		item.inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;
		item.setCurrentFile("PracticeRoomSolution.java");
		item.getIDEInput().SetCode("import june.*;\n\npublic class PracticeRoomSolution extends Spell{\n\n    public void cast(){\n\n\n    }\n}");
		
		Debug.Log ("Adding item to inventory");
		//Add the CodeScrollItem to the inventory
		GameObject.Find("Inventory").GetComponent<Inventory>().addItem(solution_scroll);
		
		Debug.Log("Done!");
	}
	
	void givePlayerASpellbook()
	{
		GameObject book = new GameObject();
		book.name = "Book";
		book.AddComponent<PracticeRoomSpellbookItem>();
		PracticeRoomSpellbookItem item = book.GetComponent<PracticeRoomSpellbookItem>();
		item.item_name = "Spells";
				
		Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
		inventory.addItem(book);
		
		PracticeRoomSpellbook spellbook = GameObject.Find("PracticeRoomSpellbook").GetComponent<PracticeRoomSpellbook>();
		
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
		spellbook.Add (new FilePage("CrossRiver", "CrossRiver/texture", "CrossRiver/code"));
		spellbook.Add (new FilePage("JumpWall", "JumpWall/texture", "JumpWall/code"));
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
		AudioSource main_audio = GameObject.Find("Voice").audio;
			
		AudioClip spellbook_clip = Resources.Load("PageTurn") as AudioClip;
		PracticeRoomSpellbook.PageTurnedForward += (page) => {
			main_audio.audio.PlayOneShot(spellbook_clip);
		};
		
		PracticeRoomSpellbook.PageTurnedBackward += (page) => {
			main_audio.audio.PlayOneShot(spellbook_clip);
		};
		
		AudioClip drop_item_clip = Resources.Load("DropItem") as AudioClip;
		Inventory.DroppedOff += (target) => {
			main_audio.audio.PlayOneShot(drop_item_clip);	
		};
		
		Enchantable.EnchantmentStarted += (target, class_name) => {
			if (firstPersonCamera != null && thirdPersonCamera != null)
			{
				firstPersonCamera.enabled = false;
				thirdPersonCamera.enabled = true;
			}
		};
		
		Enchantable.EnchantmentEnded += (target, class_name) => {
			if (firstPersonCamera != null && thirdPersonCamera != null)
			{
				System.Threading.Thread.Sleep (3000);
				firstPersonCamera.enabled = true;
				thirdPersonCamera.enabled = false;
			}

		};
	}
}
