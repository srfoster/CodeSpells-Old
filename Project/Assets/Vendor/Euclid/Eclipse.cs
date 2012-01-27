using UnityEngine;
using System.Collections;

using System.Diagnostics;



/*
 *  Why is this a MonoBehaviour when it's just a library?  So you can access it from a JavaScript program without having
 *  to put this in StandardAssets.
 * 
 *  Why not put this class in StandardAssets?  Because it relies on the Shell Project, which is part of Standard Assets but is (currently)
 *  written in JavaScript.  We can move this to Standard Assets when the Shell Project is changed.  But the Shell project is being used by
 *  the June project.  So this will require some coordination.
 * 
 *  See the Example files for usage.
 * 
 **/
public class Eclipse : MonoBehaviour {
	
	public string JavaSrcUpdateString(string file_name, string project)
	{
		Process p=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command java_src_update -p \""+project+"\" -f \""+file_name+"\" -v");
		var output = p.StandardOutput.ReadToEnd();
	    var error = p.StandardError.ReadToEnd();
		return output+"\n"+error;	
	}
	
	public string JavaCompleteString(string file_name, string project, int cursor)
	{
		Process p=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command java_complete -p \""+project+"\" -f \""+file_name+"\" -o "+cursor+" -e utf-8 -l compact");
		var output = p.StandardOutput.ReadToEnd();
	    var error = p.StandardError.ReadToEnd();
		return output+"\n"+error;	
	}
	
	public string ProjectListString()
	{
		Process p=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command projects");
		var output = p.StandardOutput.ReadToEnd();
	    var error = p.StandardError.ReadToEnd();
		return output+"\n"+error;		
	}
	
	public string CreateNewProject(string project_path)
	{
		Process p=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command project_create -f \""+project_path+"\" -n java");
		var output = p.StandardOutput.ReadToEnd();
	    var error = p.StandardError.ReadToEnd();
		return output+"\n"+error;		
	}
}