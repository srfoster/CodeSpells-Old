/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Diagnostics;

using System.Text.RegularExpressions;


public class EclipseInput : FileInput {
	string project_name;
	string project_path;
	string short_file_name;
	Eclipse eclipse;
	
	bool should_compile = false;
	Process compile_process;
	
	String current_error = "";
	//String last_error = "";
	
	public EclipseInput(string project_name, string file_name) : base(file_name)
	{

		this.project_name = project_name;
		eclipse = new Eclipse();
		
		//This should definitely be moved into Euclid's API.  Clients of an API should not have to do this much work!
		/*
		string project_list_string = eclipse.ProjectListString();
		string project_string = "";
		foreach(string s in project_list_string.Split('\n'))
		{
			Debug.Log("Project : " + s);
			if(!s.Equals(""))
			{
				string[] project_string_split = s.Split(',');
				string name = project_string_split[1].Split(':')[1].Replace("\"","");
	
				if(name.Equals(project_name))
				{
					project_path = project_string_split[2].Split(':')[1].Replace("\"","");
				}
			}
		}
		
		
		//If no Eclipse project was found, we create a new one.
		if(project_string.Equals(""))
		{
			string[] file_name_split = file_name.Split('/');
			string[] file_name_split_path = new string[file_name_split.Length-1];
			
			for(int i = 0; i < file_name_split.Length - 1; i++)
			{
				file_name_split_path[i] = file_name_split[i];
			}
				
			project_path = String.Join("/",file_name_split_path);
			
			eclipse.CreateNewProject(project_path);
		}
		
		
		
		short_file_name = GetFileName().Replace(project_path,"");
		*/
		
		/*
		Thread java_thread = (new Thread(javaCompile));
		java_thread.Start();
		*/
	}
	

	override public void Process(IDE ide)
	{
		//Right now we just print out the build errors and the completions
		//  Ulitimately, we'll need to parse this (on the Euclid side) and display the information to the user somehow.
		//  But I'm not really sure where to put the build errors in our IDE.
		//  And I'm not sure how I want to implement the drop-down menu for the code completion.
		//In any event, we are fetching the info we need.  We just need to massage it and display.
		
	
		
		/*
		string messages = eclipse.JavaSrcUpdateString(short_file_name,project_name);
		Debug.Log(messages);
		
		int cursor = ide.GetCursorPosition();
		
		string completions=eclipse.JavaCompleteString(short_file_name,project_name,cursor);
		Debug.Log(completions);
		*/
				
		javaCompile();
		
		ide.setErrorMessage(noviceClean(current_error));
		
		ide.clearStyles();

		
      	string pat = @":([0-9]+)";

      	// Instantiate the regular expression object.
     	Regex r = new Regex(pat);

      	// Match the regular expression pattern against a text string.
     	Match m = r.Match(current_error);
      	int matchCount = 0;
      	while (m.Success) 
		{
			Capture c = m.Groups[1].Captures[0];

			
			int line_num = int.Parse(c.ToString());
			
			ide.addStyle(line_num - 1, "error");
			
			m = m.NextMatch();
		}
	}
	
	virtual public void javaCompile()
	{			
		try{					
			compile_process = Shell.shell_no_start("javac", "-classpath \"" + JuneConfig.june_files_path + "\" "+GetFileName());
			compile_process.Start();	
			
			var output = compile_process.StandardOutput.ReadToEnd();
	   		var error = compile_process.StandardError.ReadToEnd();
			
			current_error = error;		
			
			// free resources
			compile_process.WaitForExit();
			compile_process.Close();

		}catch(Exception e){
		}
	}
	
	string noviceClean(string java_error_string)
	{
		
		Regex r = new Regex(".*\\^.*");
		string[] errors = r.Split(java_error_string);
		
		string new_java_error_string = "";
		foreach(string err in errors)
		{
			string new_err = Regex.Replace(err, "/.*?:", "" );
			
			new_java_error_string += new_err + "------------\n";
		}
		
		return new_java_error_string;
	}
}
