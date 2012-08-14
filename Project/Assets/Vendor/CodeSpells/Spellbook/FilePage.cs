using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


public class FilePage : SpellbookPage {

	public FilePage(string name, string texture_location, string code_location){
		
		code = (Resources.Load(code_location) as TextAsset).text;
		
		texture = Resources.Load(texture_location) as Texture2D;
		
		this.name = name;
	}
	
	public string getName(){
	    return Regex.Replace(name, "\\d","");	
	}
	
}
