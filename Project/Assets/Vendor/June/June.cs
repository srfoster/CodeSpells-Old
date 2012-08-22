/* See the copyright information at https://github.com/srfoster/June/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Diagnostics;

public class June {
	
	protected GameObject game_object;
	protected string game_object_name;
	protected string java_file_name;
	
	protected Process java_process;
	protected Thread java_thread;
	
	public delegate void GameObjectCallback(GameObject obj);
	
	protected GameObjectCallback callback = null;
	
	protected bool is_stopped = false;
	
	protected string object_id;
	
	public static bool isPlaying = true;
	
	
	private bool success = true;

		
	public June(GameObject game_object, string java_file_name)
	{
		this.game_object = game_object;
		
		this.game_object_name = game_object.name;
		this.java_file_name = java_file_name;
		
		this.object_id = game_object.GetInstanceID().ToString();
	}
	
	public void setObjectId(string id)
	{
		this.object_id = id;	
	}

	public void Start() {
		success = true;
		java_thread = (new Thread(javaCompileAndRun));
		java_thread.IsBackground = true;
		java_thread.Start();
	}
	
	public bool isStopped()
	{
		return is_stopped;	
	}
	
	public bool wasSuccessful()
	{
		return success;	
	}
	
	public void Stop() {
		if(is_stopped)
			return;
		
		//is_stopped = true;
		
		success = true;
				
		try{
			//Work-around because Unity is being stupid
			java_process.GetType().GetMethod( "Kill" ).Invoke( java_process, new object[]{} );
		}catch(Exception e){
			UnityEngine.Debug.Log(e);
		}
		
		
	}
	
	public string getFileName()
	{
		return java_file_name;	
	}
	
	virtual public void javaCompileAndRun()
	{

		
		try{
			
			string class_name = java_file_name.Split('.')[0];
			
			
			Process compile_process = Shell.shell_no_start("javac", "-classpath \"" + JuneConfig.june_files_path + "\" "+JuneConfig.java_files_path+"/"+java_file_name);
			compile_process.Start();	
			
			var output = compile_process.StandardOutput.ReadToEnd();
	   		var error = compile_process.StandardError.ReadToEnd();
			
	//		Popup.mainPopup.popup(""  + output + " " + error);

			if(!error.Equals(""))
			{
				success = false;	
			}
			
						
			Process test = Shell.shell_no_start("java", "-version");
			test.Start();	
			
			var output2 = test.StandardOutput.ReadToEnd();
	   		var error2 = test.StandardError.ReadToEnd();
			
			UnityEngine.Debug.Log (output2 + " " + error2);
			
			
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			{
				UnityEngine.Debug.Log("java " + "-classpath \"" + JuneConfig.june_files_path + ";" + JuneConfig.java_files_path+"\" june.Caster "+class_name+" \"" + object_id +"\"");
				java_process = Shell.shell_no_start("java", "-classpath \"" + JuneConfig.june_files_path + ";" + JuneConfig.java_files_path+"\" june.Caster "+class_name+" \"" + object_id +"\"");
			}
			else
			{
				UnityEngine.Debug.Log("java " + "-classpath \"" + JuneConfig.june_files_path + ":" + JuneConfig.java_files_path+"\" june.Caster "+class_name+" \"" + object_id +"\"");
				java_process = Shell.shell_no_start("java", "-classpath \"" + JuneConfig.june_files_path + ":" + JuneConfig.java_files_path+"\" june.Caster "+class_name+" \"" + object_id +"\"");	
			}
			java_process.Start();
			
			var output3 = java_process.StandardOutput.ReadToEnd();
	   		var error3 = java_process.StandardError.ReadToEnd();
			
			UnityEngine.Debug.Log (output3 + " " + error3);
			
			
			Boolean has_exited = Convert.ToBoolean(java_process.GetType().GetProperty( "HasExited" ).GetValue(java_process, new object[]{}));
			while(!has_exited )
			{
				if(!isPlaying)
					Stop();
				
				//UnityEngine.Debug.Log("Waiting for Java process to exit: ");	
				
				Thread.Sleep(500);
				
				has_exited = Convert.ToBoolean(java_process.GetType().GetProperty( "HasExited" ).GetValue(java_process, new object[]{}));
				


			}
		
							
			if(java_process.ExitCode != 0)
			{
				success = false;
			}

			
			is_stopped = true;
		}catch(Exception e){
			UnityEngine.Debug.Log(e);
		}
	}

}