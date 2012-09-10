using UnityEngine;
using System.Collections;

public class CrateGUI : MonoBehaviour {
	
	int client_count = 0;
	int server_count = 0;
	GameObject selectedPlayer;
	
	void Start()
	{
		name = "CrateGUI";
		if(Network.isClient)
		{
			selectedPlayer = GameObject.Find("Dueling First Person Controller Client(Clone)");
		}
		else
		{
			selectedPlayer = GameObject.Find("Dueling First Person Controller(Clone)");	

		}
		
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
		
		float frameInterval = Time.deltaTime;
		if(Network.isClient && client_count > 30)
		{
			decrementHealth (frameInterval, selectedPlayer);
		}
		else if(Network.isServer && server_count > 30)
		{
			decrementHealth (frameInterval, selectedPlayer);
		}
			
	}
	
	void decrementHealth(double amount, GameObject gameObject) {
			gameObject.GetComponent<Health>().decreaseHealth(amount);
	}

}
