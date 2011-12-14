/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class StringInput : IDEInput {
	public string code_string = "some code";
	public string file_name   = "file name";
	
	public StringInput(string code_string, string file_name)
	{
		this.code_string = code_string;
		this.file_name   = file_name;
	}
	
	public override string GetCode()
	{
		return code_string;	
	}
	
	public override string GetFileName()
	{
		return file_name;	
	}
	
	public override void SetCode(string code_string)
	{
		this.code_string = code_string;
	}
	
	public override void Process(IDE ide)
	{
		
	}
	                         
}
