using UnityEngine;
using System.Collections;
using System;

public class NPCQuestTalk : MonoBehaviour {
	private Conversation [] conversationList;
	private QuestChecker [] questList;
	
	public string convo_files;
	public string quests;
	
	enum whichQuest {Fire=1, PickUp, River, Extinguish, Levitate, Fly, Transport, None}; 
	private int questIndex;
	
	private QuestChecker questObject;
	private Conversation convo;
	
	private int currentQuest;
	
	void Start()
	{
		string [] questIndices = quests.Split(';');
		questList = new QuestChecker [questIndices.Length];
		for(int i = 0; i< questIndices.Length; i++ )
		{
			string quest = questIndices[i];
			questIndex = Int32.Parse(quest);
			
			switch((whichQuest) questIndex)
			{	
			case whichQuest.Fire:
				questObject = new FireQuestChecker();
				break;
			case whichQuest.PickUp:
				questObject = new PickUpQuestChecker();
				break;
			case whichQuest.River:
				questObject = new RiverQuestChecker();
				break;
			case whichQuest.Extinguish:
				questObject = new ExtinguishQuestChecker();
				break;
			case whichQuest.Levitate:
				questObject = new LevitateQuestChecker();
				break;
			case whichQuest.Fly:
				questObject = new FlyQuestChecker();
				break;
			case whichQuest.Transport:
				questObject = new TransportQuestChecker();
				break;
			default:
				questObject = new NonQuestChecker();
				break;
			}
			questList[i] = questObject;
		}
		
		string [] conversation_files = convo_files.Split(';');
		conversationList = new Conversation [conversation_files.Length];
		for(int i = 0; i< conversation_files.Length; i++ )
		{
			convo = new GraphConversation(conversation_files[i]);
			conversationList[i] = convo;
		}
		currentQuest = 0;
	}
	
	void OnMouseDown()
	{
		ConversationDisplayer c = GameObject.Find("ConversationDisplayer").GetComponent(typeof(ConversationDisplayer)) as ConversationDisplayer;
		Time.timeScale = 0;
		if(currentQuest < questList.Length && questList[currentQuest].checkIfCompleted())
		{
			currentQuest++;
		}
		
		c.Converse(((Conversation)conversationList[currentQuest]));	
	}
}