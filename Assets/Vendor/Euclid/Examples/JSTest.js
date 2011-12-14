/* See the copyright information at https://github.com/srfoster/Euclid/blob/master/COPYRIGHT */

import System.Threading;

private var messages : String;
private var scrollPosition : Vector2;
private var eclipse;

function Start()
{
	eclipse = GameObject.Find("EclipseObject").GetComponent("Eclipse");
	(new Thread(AskEclipse)).Start();
}

function AskEclipse()
{
	while(true)
	{
		messages = eclipse.ProjectListString();
		Thread.Sleep(200);
	}
}

function OnGUI()
{		
	GUI.Label(new Rect(260,5,50,20), "JS Test:");
	
	GUI.TextArea(new Rect(260, 25, 250, 250), messages);

}