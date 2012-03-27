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
		
	public June(GameObject game_object, string java_file_name)
	{
		this.game_object = game_object;
		this.game_object_name = game_object.name;
		this.java_file_name = java_file_name;
	}

	public void Start() {
		java_thread = (new Thread(javaCompileAndRun));
		java_thread.Start();
	}
	
	public bool isStopped()
	{
		return is_stopped;	
	}
	
	public void Stop() {
		if(is_stopped)
			return;
		
		//is_stopped = true;
		
		
		UnityEngine.Debug.Log("Trying to kill");
		
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
			
			Shell.shell("javac", "-classpath '" + JuneConfig.june_files_path + "' "+JuneConfig.java_files_path+"/"+java_file_name);
		

			java_process = Shell.shell_no_start("java", "-classpath '" + JuneConfig.june_files_path + ":" + JuneConfig.java_files_path+"' "+class_name+" " + game_object_name);	
			java_process.Start();
			
			Boolean has_exited = Convert.ToBoolean(java_process.GetType().GetProperty( "HasExited" ).GetValue(java_process, new object[]{}));
			while(!has_exited)
			{
				UnityEngine.Debug.Log("Waiting for Java process to exit: ");	
				
				Thread.Sleep(500);
				
				has_exited = Convert.ToBoolean(java_process.GetType().GetProperty( "HasExited" ).GetValue(java_process, new object[]{}));
			}
			
			UnityEngine.Debug.Log("I'm here.");
	
			is_stopped = true;
		}catch(Exception e){
			UnityEngine.Debug.Log(e);
		}
	}

}