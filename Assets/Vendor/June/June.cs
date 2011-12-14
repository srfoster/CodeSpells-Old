/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System;
using System.Threading;


public class June {
	
	private GameObject game_object;
	private string game_object_name;
	private string java_file_name;
	
	public June(GameObject game_object, string java_file_name)
	{
		this.game_object = game_object;
		this.game_object_name = game_object.name;
		this.java_file_name = java_file_name;
	}

	public void Start () {
		(new Thread(javaCompileAndRun)).Start();
	}
	
	public void javaCompileAndRun()
	{
		string class_name = java_file_name.Split('.')[0];
		
		Shell.shell("javac", "-classpath '" + JuneConfig.june_files_path + "' "+JuneConfig.java_files_path+"/"+java_file_name);
		var o = Shell.shell("java", "-classpath '" + JuneConfig.june_files_path + ":" + JuneConfig.java_files_path+"' "+class_name+" " + game_object_name);	
		
		Debug.Log("Ran java: " + o);
	}
}