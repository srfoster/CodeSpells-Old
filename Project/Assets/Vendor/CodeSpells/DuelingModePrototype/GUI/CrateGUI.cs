using UnityEngine;
using System.Collections;

public class CrateGUI : MonoBehaviour {
	
	int client_count = 0;
	int server_count = 0;
	
	[RPC]
	public void IncrementCrateCount()
	{
		if(Network.isClient)
		{
			networkView.RPC("IncrementCrateCount", RPCMode.Server);
		}
		else
		{
			client_count++;
			
			networkView.RPC("CrateCountChangedClient", RPCMode.Others, client_count);
		}		
	}
	
	[RPC]
	public void CrateCountChangedClient(int count)
	{
		client_count = count;
	}

	
	void OnGUI()
	{
		GUI.Label (new Rect (10, Screen.height - 75, 100, 20), ""+client_count);
		GUI.Label (new Rect (10, Screen.height - 85, 100, 20), ""+server_count);
	}

}
