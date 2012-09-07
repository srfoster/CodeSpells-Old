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
		{
			server_count++;
			networkView.RPC("TellClientToIncrementServer", RPCMode.Others);
		}		
	}
	
	[RPC]
	public void TellClientToIncrementServer(int count)
	{
		server_count ++;
	}
	
	[RPC]
	public void TellServerToIncrementClient(int count)
	{
		client_count ++;
	}

	
	void OnGUI()
	{
		GUI.Label (new Rect (10, Screen.height - 75, 100, 20), ""+client_count);
		GUI.Label (new Rect (10, Screen.height - 85, 100, 20), ""+server_count);
	}

}
