using UnityEngine;
using System.Collections;

public class SpellbookPage {
	public string code = "";
	public Texture2D texture;
	public string name;
	
	public SpellbookPage(){
		
	}
	
	virtual public IEnumerator load(){
		return null;
	}
	
}
