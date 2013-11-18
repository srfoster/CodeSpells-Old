using UnityEngine;
using System.Collections;

public class Response {
	
	private string response = null;
	private int nextNode = -1;
	private int prevNode = -1;
	private bool is_exit = false;
	private string starts_quest = "";

	public Response(string p_response, int p_prevNode, int p_nextNode, bool exit, string starts_quest)
	{
		response = p_response;
		nextNode = p_nextNode;
		prevNode = p_prevNode;
		is_exit = exit;
		this.starts_quest = starts_quest;
	}
	
	public string getResponseText()
	{
		return response;	
	}
	
	public int getNextNode()
	{
		return nextNode;	
	}
	
	public int getPrevNode()
	{
		return prevNode;	
	}
	
	public void setIsExit(bool p_is_exit)
	{
		this.is_exit = p_is_exit;	
	}
	
	public bool isExit()
	{
		return this.is_exit;
	}

	public string startsQuest()
	{
		return this.starts_quest;
	}
	
	public void setNextNode(int p_nextNode)
	{
		this.nextNode = p_nextNode;
	}
}