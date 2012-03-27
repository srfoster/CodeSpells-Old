using UnityEngine;
using System.Collections;

public abstract class Conversation {

	public abstract string GetText();
	public abstract ArrayList GetResponses();
	public abstract void Respond(Response response);
	public abstract void endConversation();
}
