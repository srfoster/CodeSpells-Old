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
	
	public override void SetCode(string code_string)
	{
		StreamWriter sw = new StreamWriter(file_name, false);
	    sw.Write(code_string);
	    sw.Close();
	}
	
	public override void Process(IDE ide)
	{
		
	}
	
}
