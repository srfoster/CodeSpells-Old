using UnityEngine;
using System.Collections;
using System;

public class NPCQuestTalk : MonoBehaviour {
	private Conversation [] conversationList;
	private QuestChecker [] questList;
	
	public string convo_files;
	public string quests;
	
	enum whichQuest {Fire=1, PickUp, River, Extinguish, Levitate, Fly, Transport, None, Staff}; 
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
			case whichQuest.Staff:
				questObject = new StaffUnlocker();
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
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;
		if(Vector3.Distance(transform.position, player.position)<50)
			StartCoroutine(turnToFaceAndTalk());
	}
	
	IEnumerator turnToFaceAndTalk()
	{
		//Does an sudden, jumpy turn which actually looks okayish.  But this method is called as a
		// coroutine in order to facilitate a smoother turn if we want to implement that.
		transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
		
		yield return new WaitForSeconds(0.2f);

		ConversationDisplayer c = GameObject.Find("ConversationDisplayer").GetComponent(typeof(ConversationDisplayer)) as ConversationDisplayer;
		Time.timeScale = 0;
		if(currentQuest < questList.Length && questList[currentQuest].checkIfCompleted())
		{
			currentQuest++;
		}
		
		c.Converse(((Conversation)conversationList[currentQuest]));
		
		c.show(GameObject.Find("Inventory"));
	}
}