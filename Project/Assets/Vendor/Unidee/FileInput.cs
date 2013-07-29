/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System.IO;

public class FileInput : IDEInput {
	string file_name;
	
	public FileInput(string file_name)
	{
		this.file_name   = file_name;
	}
	
	public override string GetCode()
	{
		return File.ReadAllText(file_name);
	}
	
	public override string GetFileName()
	{
		return file_name;	
	}
	
	public string GetSpellName()
	{
	    char[] seps = {'.'};
	    string s = file_name.Substring(file_name.LastIndexOf("/")+1);
	    return s.Split(seps)[0];
	}
	
	public override void SetCode(string code_string)
	{
		Debug.Log("********************IN FILEINPUT SETCODE**********************");
		Debug.Log("filename: "+file_name);
		StreamWriter sw = new StreamWriter(file_name, false);
	    sw.Write(code_string);
	    sw.Close();
		Debug.Log("********************DONE WITH FILEINPUT SETCODE**********************");
	}
	
	public override void Process(IDE ide)
	{
		
	}
	
}
