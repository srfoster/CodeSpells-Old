using UnityEngine;
using System.Collections;

/*
 * Current gnome's conversation protocal
 */
public class NPCTalk : MonoBehaviour {
	private Conversation convo = null;
	private bool talked = false; 
	
	public string convo_file;
	enum whichQuest {Fire=1, PickUp, River, Extinguish, Levitate, Fly, Transport}; 
	public int questIndex;
	private QuestChecker quest;
	
	void Start()
	{
		switch((whichQuest) questIndex)
		{
		case whichQuest.Fire:
			quest = new FireQuestChecker();
			break;
		case whichQuest.PickUp:
			quest = new PickUpQuestChecker();
			break;
		case whichQuest.River:
			quest = new RiverQuestChecker();
			break;
		case whichQuest.Extinguish:
			quest = new ExtinguishQuestChecker();
			break;
		case whichQuest.Levitate:
			quest = new LevitateQuestChecker();
			break;
		case whichQuest.Fly:
			quest = new FlyQuestChecker();
			break;
		case whichQuest.Transport:
			quest = new TransportQuestChecker();
			break;
		default:
			quest = new NonQuestChecker();
			break;
		}
	}
	
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
		
		// Begin the conversation displayer
		c.Converse(convo, quest);
	}
	
	void OnTriggerExit (Collider collider) {
				
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		if(collider.gameObject != player)
			return;
		
		ConversationDisplayer c = GameObject.Find("ConversationDisplayer").GetComponent(typeof(ConversationDisplayer)) as ConversationDisplayer;
		// Set the conversation to the proper return point
		convo.resetConversation(false);
		
		// End the conversation displayer
		c.Converse(null, quest);
	}
}
