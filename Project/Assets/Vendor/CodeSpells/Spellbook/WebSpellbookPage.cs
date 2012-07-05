using UnityEngine;
using System.Collections;

public class WebSpellbookPage : SpellbookPage {
	string path;

	public WebSpellbookPage(string path, string name)
	{
		this.path = path;
		this.name = name;
	}
	
	override public IEnumerator load()
	{
		
		string texture_path = path + name + "/texture.png";
		string code_path = path + name + "/code.txt";
		


		
		WWW www = new WWW(texture_path);
        yield return www;
        texture = www.texture;
		

			
		WWW www2 = new WWW(code_path);
        yield return www2;
		code = System.Text.Encoding.UTF8.GetString(www2.bytes);	
		

	}
}
