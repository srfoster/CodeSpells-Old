using UnityEngine;
using System.Collections;

public class CodeScrollItem : SpellItem {
	
	public string file_name;
	
	public override void DroppedOnInventory(Vector3 mousePosition){
		SetHidden(false);
		
		IDEInput input = new EclipseInput("CodeSpellsJava", Application.dataPath + "/Vendor/CodeSpells/Java/"+file_name);
		IDE ide = (GameObject.Find("IDE").GetComponent("IDE") as IDE);
		ide.SetInput(input);
		ide.show(GameObject.Find("Inventory"));
	}
	
	public override void DroppedOn(GameObject target)
	{
		if(target == null)
		{
			SetHidden(false);
			return;
		}
		
		June june = new June(target, file_name);
		june.Start();
		
		
		// Because we extend SpellScroll we get a nice animation.
		// But we'll probably want different animations in hte futher, so we'll end up changing this.
		base.DroppedOn(target);
	}
}
