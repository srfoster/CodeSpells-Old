using UnityEngine;
using System.Collections;

/*
 * Conversation with data stored as a graph of Nodes and Responses.
 */
public class GraphConversation : Conversation {
	private Node initialNode = null;
	private Node currentNode = null;
	private Graph g = null;
	private readonly bool debug = true;
	private static readonly int INIT = 0;
	
	
	
	public GraphConversation(string convo_file)
	{
		g = new Graph(convo_file);
		initialNode = g.getNode(INIT);
		currentNode = initialNode;
	}
	
	//Returns the text that the NPC will say to the player
	public override string GetText(){
		return currentNode.getStatement();
	}
	
	//Returns the list of responses that the player can say to the NPC
	public override ArrayList GetResponses(){
		if(currentNode == null)
			return null;
		
		return g.getResponses(currentNode.getId());
	}
	
	//Continues the conversation based on what the player responded
	public override void Respond(Response response){
		if(debug)
			Debug.Log("Previous Current Node: "+currentNode.getId());
		
		currentNode = g.getNode(response.getNextNode());
		
		if(debug)
			Debug.Log("Current Node: "+currentNode.getId());
	}
	
	// Sets up the conversation so that when the player returns, the
	// conversation continues properly
	public override void endConversation()
	{
		currentNode = g.getReentryNode(currentNode.getId());	
	}
}
