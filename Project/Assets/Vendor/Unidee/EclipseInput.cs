/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System;

public class EclipseInput : FileInput {
	string project_name;
	string project_path;
	string short_file_name;
	Eclipse eclipse;

	
	public EclipseInput(string project_name, string file_name) : base(file_name)
	{

		this.project_name = project_name;
		eclipse = new Eclipse();
		
		//This should definitely be moved into Euclid's API.  Clients of an API should not have to do this much work!
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
	}
	

	override public void Process(IDE ide)
	{
		//Right now we just print out the build errors and the completions
		//  Ulitimately, we'll need to parse this (on the Euclid side) and display the information to the user somehow.
		//  But I'm not really sure where to put the build errors in our IDE.
		//  And I'm not sure how I want to implement the drop-down menu for the code completion.
		//In any event, we are fetching the info we need.  We just need to massage it and display.
		
	
		
		
		string messages = eclipse.JavaSrcUpdateString(short_file_name,project_name);
		Debug.Log(messages);
		
		int cursor = ide.GetCursorPosition();
		
		string completions=eclipse.JavaCompleteString(short_file_name,project_name,cursor);
		Debug.Log(completions);
	}
}
