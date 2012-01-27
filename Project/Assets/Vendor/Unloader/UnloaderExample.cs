/* See the copyright information at https://github.com/srfoster/Unloader/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System.Threading;

public class UnloaderExample : MonoBehaviour {
	
	Unloader unloader;
	
	// Use this for initialization
	void Start () {
		unloader = GetComponent("Unloader") as Unloader;
		(new Thread(load)).Start();
	}
	
	void load()
	{
		Thread.Sleep(5000);
		unloader.Loaded();	
	}
	

}
