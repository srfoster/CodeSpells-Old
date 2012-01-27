/* See the copyright information at https://github.com/srfoster/Euclid/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;
using System.Threading;

public class CSTest : MonoBehaviour {
	
	private string messages;
	private Vector2 scrollPosition;
	
	void Start()
	{
		(new Thread(AskEclipse)).Start();
	}
	
	void AskEclipse()
	{
		while(true)
		{
			Eclipse eclipse = new Eclipse();
			messages = eclipse.ProjectListString();
			Thread.Sleep(200);
		}
	}

	void OnGUI()
	{		
		GUI.Label(new Rect(5,5,50,20), "C# Test:");
		
		GUI.TextArea(new Rect(5, 25, 250, 250), messages);
	
	}
}
