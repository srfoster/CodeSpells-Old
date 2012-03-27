using UnityEngine;
using System.Collections;

/*
 * Current gnome's conversation protocal
 */
public class NPCTalk : MonoBehaviour {
	private Conversation convo = null;
	private bool talked = false; 
	
	public string convo_file;
	
	void OnTriggerEnter (Collider collider) {
		
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if(collider.gameObject != player)
			return;
				
		// TODO: Need to freeze the player
		
		
		// Initialize a conversation if one hasn't started yet
		if(!talked)
		{
			talked = true;
			convo = new GraphConversation(convo_file);
		}
		
		ConversationDisplayer c = GameObject.Find("ConversationDisplayer").GetComponent(typeof(ConversationDisplayer)) as ConversationDisplayer;
		
		// TODO: Need to determine if the action was completed
		
		// Begin the conversation displayer
		c.Converse(convo);
		
	}
	
	void OnTriggerExit (Collider collider) {
				
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if(collider.gameObject != player)
			return;
		
		ConversationDisplayer c = GameObject.Find("ConversationDisplayer").GetComponent(typeof(ConversationDisplayer)) as ConversationDisplayer;
		// Set the conversation to the proper return point
		convo.endConversation();
		
		// End the conversation displayer
		c.Converse(null);
	}
}
