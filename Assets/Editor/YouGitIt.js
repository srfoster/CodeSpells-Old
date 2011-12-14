/* See the copyright information at https://github.com/srfoster/YouGitIt/blob/master/COPYRIGHT */

/*
TODO: Make it actually fetch the correct file.
*/

import System;
import System.IO;
import System.Net;
import System.Threading;

class YouGitIt extends EditorWindow {

	var button_text = "Git It!";
	var data_path = Application.dataPath;
	var feedback = "";
	
    @MenuItem ("Window/YouGitIt")
    static function ShowWindow () {


        EditorWindow.GetWindow (YouGitIt);
    }

    function OnGUI () {
		if(GUI.Button(Rect(Screen.width/2 - 50,Screen.height/2-30,100,20),button_text) && button_text != "Gitting...")
		{
			button_text = "Gitting...";
			(new Thread(GitIt)).Start();
		}
		
		GUI.Label(Rect(50,Screen.height/2,Screen.width - 50, 50), feedback); 
    }
    
    function GitIt()
    {
    	try{
	        var path = data_path+"/Dependencies";    
		    var info = new DirectoryInfo(path);
		    var fileInfo = info.GetFiles();
		
			
		    for (file in fileInfo)
		    {
		        var filename = file.ToString(); 
		    	var to_gits = System.IO.File.ReadAllText(filename).Split('\n'[0]);
		    	
		    	for(var to_git in to_gits)
		    	{
		    		if(to_git != "")
		    		{
		    			try{
							var to_git_path_split = to_git.Split('/'[0]);
							var package_name = to_git_path_split[to_git_path_split.Length - 1];
							
					    	Debug.Log("Gitting " + to_git);
							feedback = "Gitting: " + to_git;
				    		
				    		var webClient = new WebClient();
							webClient.DownloadFile(to_git, data_path + "/Vendor/YouGitIt/"+package_name);
						}catch(e){
							Debug.Log(e);
							feedback = e.ToString();
						}
					}
		    	}
		    	
		    	
		    }
		
			button_text = "Git It!";
			feedback = "Got It!  Refresh your Vendor/YouGitIt/ folder";
		}catch(e){
			feedback = e.ToString();
		}
    } 
}