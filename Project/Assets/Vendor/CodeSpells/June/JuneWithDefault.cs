using UnityEngine;
using System.Collections;
using System.IO;

/*
 * Same as June except that the first thing it does is overwrite the java file with
 * the source contained in source_file_name.  This is necessary for setting up the levels
 * so that they are the same each time.  We don't want the user's past actions to affect
 * the current game state.
 */

public class JuneWithDefault : June {
	
	private string source_file_name;
	
	public JuneWithDefault(GameObject game_object, string java_file_name, string source_file_name) : base(game_object, java_file_name)
	{
		this.source_file_name = source_file_name;
	}
	
	
	override public void javaCompileAndRun()
	{
		string code = File.ReadAllText(source_file_name);
				
		string full_path = JuneConfig.java_files_path+"/"+java_file_name;
		
		StreamWriter sw = new StreamWriter(full_path, false);
	    sw.Write(code);
	    sw.Close();	
				
		base.javaCompileAndRun();
	}
}
