using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class SpellbookPage {
	public string code = "";
	public Texture2D texture;
	public string name;
	
	public SpellbookPage(){
		
	}
	
	virtual public IEnumerator load(){
		return null;
	}
	
	public string getName(){
	    return Regex.Replace(name, "\\d","");	
	}
	
}
