using UnityEngine;
using System.Collections;

public class CrateGUI : MonoBehaviour {
	
	int client_count = 0;
	int server_count = 0;
	
	void Start()
	{
		name = "CrateGUI";
		
	}
	
	[RPC]
	public void IncrementCrateCount()
	{
		if(Network.isClient)
		{
			networkView.RPC("TellServerToIncrementClient", RPCMode.Server);
			client_count++;
			
		}
		else
		{
			server_count++;
			networkView.RPC("TellClientToIncrementServer", RPCMode.Others);
		}		
	}
	
	[RPC]
	public void TellClientToIncrementServer()
	{
		server_count ++;
	}
	
	[RPC]
	public void TellServerToIncrementClient()
	{
		client_count ++;
	}

	
	void OnGUI()
	{
		GUIStyle red = new GUIStyle();
		GUIStyle blue = new GUIStyle();
		red.normal.textColor = Color.red;
		blue.normal.textColor = Color.blue;
		red.fontSize = 20;
		blue.fontSize = 20;
		
		GUI.Label (new Rect (260, Screen.height - 30, 100, 20), ""+client_count, blue);
		GUI.Label (new Rect (260, Screen.height - 50, 100, 20), ""+server_count, red);
	}

}
