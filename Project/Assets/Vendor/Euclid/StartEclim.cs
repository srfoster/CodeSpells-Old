
/**

The Euclid Project

0) Depends on the Unloader Project -- GUI loading indicators inside of Unity.  And the Shell Project.
1) Eclipse Object + StartEclim script  //Starts Eclim on startup.  Takes a while.
	A) Creates a new Unloader.  Tells it when Eclim has loaded.
2) Eclipse class is also attached to the the Eclipse object.
	A) Provides API for running Eclim commands.
	B) Returns null if Eclim server has not loaded (must get confirmation from the StartEclim script).
	C) Calls shell() and communicates with Nailgun.
		i) The caller is responsible for handling the calls to the Eclipse API.  They might block.  Should start new thread.


How to manage dependencies?  I want to take the Shell Project out of June and Euclid.  Just put dependencies in the readme?  Or include an "installer"?

**/

using UnityEngine;
using System.Threading;
using System;

public class StartEclim : MonoBehaviour
{
	private Unloader unloader;
	
	void Start () {
		unloader = GetComponent("Unloader") as Unloader;
		(new Thread(startStuff)).Start();
	}
	
	void startStuff()
	{
		Eclipse eclipse = new Eclipse();
		string eclim_running = eclipse.ProjectListString();
	
		if(eclim_running.Contains("Connection refused"))
		{
			(new Thread(startEclimd)).Start();
			
			while(eclim_running.Contains("Connection refused"))
			{
				eclim_running = eclipse.ProjectListString();
				
				Thread.Sleep(10);
			}
		}
		
		unloader.Loaded();

	}
	
	void startEclimd()
	{
		print("Starting Eclim");
		Shell.shell("bash", EuclidConfig.eclipse_path + "/eclimd");
	}

	
}