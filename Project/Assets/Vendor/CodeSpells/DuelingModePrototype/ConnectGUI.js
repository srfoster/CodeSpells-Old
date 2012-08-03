//DontDestroyOnLoad(this);
var remoteIP = "127.0.0.1";
var remotePort = 25001;
var listenPort = 25000;
var remoteGUID = "";
var useNat = false;
private var connectionInfo = "";

var is_server = false;

function OnGUI ()
{
	GUILayout.Space(10);
	GUILayout.BeginHorizontal();
	GUILayout.Space(10);
	if (Network.peerType == NetworkPeerType.Disconnected)
	{
		useNat = GUILayout.Toggle(useNat, "Use NAT punchthrough");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);

		GUILayout.BeginVertical();
		if (GUILayout.Button ("Connect"))
		{
			if (useNat)
			{
				if (!remoteGUID)
					Debug.LogWarning("Invalid GUID given, must be a valid one as reported by Network.player.guid or returned in a HostData struture from the master server");
				else
					Network.Connect(remoteGUID);
			}
			else
			{
				Network.Connect(remoteIP, remotePort);
			}
		}
		if (GUILayout.Button ("Start Server"))
		{
			is_server = true;
			Network.InitializeServer(32, listenPort, useNat);
			// Notify our objects that the level and the network is ready
			GameObject.Find("SpawnServer").SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);		
		}
		GUILayout.EndVertical();
		if (useNat)
		{
			remoteGUID = GUILayout.TextField(remoteGUID, GUILayout.MinWidth(145));
		}
		else
		{
			remoteIP = GUILayout.TextField(remoteIP, GUILayout.MinWidth(100));
			remotePort = parseInt(GUILayout.TextField(remotePort.ToString()));
		}
	}
	else
	{
		if (useNat)
			GUILayout.Label("GUID: " + Network.player.guid + " - ");
		GUILayout.Label("Local IP/port: " + Network.player.ipAddress + "/" + Network.player.port);
		GUILayout.Label(" - External IP/port: " + Network.player.externalIP + "/" + Network.player.externalPort);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Disconnect"))
			Network.Disconnect(200);
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndHorizontal();
}

function OnServerInitialized()
{
	if (useNat)
		Debug.Log("==> GUID is " + Network.player.guid + ". Use this on clients to connect with NAT punchthrough.");
	Debug.Log("==> Local IP/port is " + Network.player.ipAddress + "/" + Network.player.port + ". Use this on clients to connect directly.");
}

function OnConnectedToServer() {
  if(!is_server)
	GameObject.Find("SpawnClient").SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
}

function OnDisconnectedFromServer () {
	if (this.enabled != false)
		Application.LoadLevel(Application.loadedLevel);
}
