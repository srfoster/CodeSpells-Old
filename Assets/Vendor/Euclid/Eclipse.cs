using UnityEngine;
using System.Collections;


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
		string messages=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command java_src_update -p \""+project+"\" -f \""+file_name+"\" -v");
		return messages;	
	}
	
	public string JavaCompleteString(string file_name, string project, int cursor)
	{
		string messages=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command java_complete -p \""+project+"\" -f \""+file_name+"\" -o "+cursor+" -e utf-8 -l compact");
		return messages;
	}
	
	public string ProjectListString()
	{
		string messages=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command projects");
		return messages;
	}
	
	public string CreateNewProject(string project_path)
	{
		string messages=Shell.shell("bash","\""+EuclidConfig.eclipse_path+"/eclim\" --nailgun-port 9091 -command project_create -f \""+project_path+"\" -n java");
		return messages;
	}
}