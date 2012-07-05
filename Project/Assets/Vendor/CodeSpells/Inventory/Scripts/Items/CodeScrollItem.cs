using UnityEngine;
using System.Collections;

public class CodeScrollItem : DraggableItem {
	
	public string file_name;
	
	private int current_frame = 0;
	
	private bool animate = false;
	
	private Texture2D still_icon;
	private Texture2D[] animated_icon;
	
    override protected void Drag(){
		base.Drag();
		
		GameObject obj = objectUnderCursor();
		if(obj != null){
			if(obj.GetComponent<Enchantable>() != null) {
				Highlighter.highlight(obj, Color.green);
			} else {
				Highlighter.highlight(obj, Color.red);
			}
		} else {
			Highlighter.highlight(null, Color.black);
		}
	}
	
	override public Texture2D getTexture()	
	{
		if(still_icon == null)
			still_icon = Resources.Load("Textures/Scroll") as Texture2D;
		
		if(animated_icon == null)
		{
			animated_icon = new Texture2D[3];
			animated_icon[0] = Resources.Load("SparkleScroll1") as Texture2D;
			animated_icon[1] = Resources.Load("SparkleScroll2") as Texture2D;	
			animated_icon[2] = Resources.Load("SparkleScroll3") as Texture2D;	
		}
		                     
		
		if(animate)
		{
			current_frame = (current_frame + 1) % 30;
	
			return animated_icon[(current_frame / 10)];
		} else {
			return still_icon;
		}
	}
	
	public override void DroppedOnInventory(Vector3 mousePosition){
		SetHidden(false);
		
		if(isEmpty())
			return;

		IDEInput input = getIDEInput();
		IDE ide = (GameObject.Find("IDE").GetComponent("IDE") as IDE);
		ide.SetInput(input);
		ide.show(GameObject.Find("Inventory"));
	}
	
	public IDEInput getIDEInput(){
		return new EclipseInput("CodeSpellsJava", Application.dataPath + "/Vendor/CodeSpells/CodeSpellsJava/"+file_name);
	}
	
	public override void DroppedOn(GameObject target)
	{
		if(target == null)
		{
			SetHidden(false);
			return;
		}
		
		if(isEmpty())
		{
			absorbSpell(target);
			
		} else {
			castSpell(target);	
		}
		

	}
	
	public void absorbSpell(GameObject target)
	{
		SetHidden(false);
		
		Enchantable enchantable = (target.GetComponent("Enchantable") as Enchantable);
		
		if(enchantable == null || !enchantable.isEnchanted())
		{
			(GameObject.Find("Popup").GetComponent("Popup") as Popup).popup("No enchantment to absorb.");
			SetHidden(false);

			return;
		}
		
		enchantable.disenchant();
		
		string file_name = enchantable.getJune().getFileName();
		setCurrentFile(file_name);

	}
	
	public void setCurrentFile(string file_name){
		this.file_name = file_name;
		this.item_name = file_name.Split('.')[0];
		this.animate = true;	
	}
	
	public void castSpell(GameObject target)
	{
		if(target.GetComponent("Enchantable") == null)
		{
			
			(GameObject.Find("Popup").GetComponent("Popup") as Popup).popup("Target ("+target.name+") immune to magic.");
			SetHidden(false);
			return;
		}
		
		June june = new June(target, file_name);
		
		SetHidden(false);
		
		item_name = "Blank";
		file_name = "";
		animate = false;

		
		inventoryTexture = Resources.Load( "Textures/Scroll") as Texture2D;

		
		(target.GetComponent("Enchantable") as Enchantable).enchant(june, delegate(GameObject t){absorbSpell(t); });
	}
		   
	private bool isEmpty()
	{
		return file_name == null || file_name.Equals("");		
	}
}
		 
