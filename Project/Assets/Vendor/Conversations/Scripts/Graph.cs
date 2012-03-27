using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Graph {
	private ArrayList nodes;
	private ArrayList edges;
	private Dictionary<Node, ArrayList> graph;
	private readonly bool debug = true;
	
	public Graph(string convo_file)
	{
		nodes = new ArrayList();
		edges = new ArrayList();
		graph = new Dictionary<Node, ArrayList>();
		
		
		readInConversation(convo_file);
	}
	
	// Reads from the conversation file where "=" is the delimeter
	public void readInConversation(string convo_file)
	{
		StringReader reader = null;
		bool readNodes = false;
		bool readEdges = false;
		bool readExits = false;
		Debug.Log("FILE: "+convo_file);
		TextAsset convo = (TextAsset)Resources.Load(convo_file, typeof(TextAsset));
		reader = new StringReader(convo.text);
		if ( reader == null )
		{
			if(debug)
		   		Debug.Log("convo.txt not found or not readable");
		}
		else
		{
			// Read each line from the file
			string txt = reader.ReadLine();
		   	while ( txt != null )
			{
				if(txt.Equals("Nodes"))
				{
					readEdges = false;
					readNodes = true;
					readExits = false;
				}
				else if(txt.Equals("Responses"))
				{
					readNodes = false;
					readEdges = true;
					readExits = false;
				}
				else if(txt.Equals("Exits"))
				{
					readNodes = false;
					readEdges = false;
					readExits = true;
				}
				else if(readNodes)
				{
					string [] split = txt.Split('='); 
					this.addNode(System.Int32.Parse(split[0]), split[1]);
				}
				else if(readEdges)
				{
					string [] split = txt.Split('='); 
					this.addEdge(System.Int32.Parse(split[0]), split[1], System.Int32.Parse(split[2]), System.Int32.Parse(split[3]));
				}
				else if(readExits)
				{
					string [] split = txt.Split('='); 
					this.addExit(System.Int32.Parse(split[0]), split[1], System.Int32.Parse(split[2]), System.Int32.Parse(split[3]));//, System.Int32.Parse(split[4]));
				}
				txt = reader.ReadLine();
			}
			if(debug)
				Debug.Log("Done reading convo.txt");
		}	
	}
	
	// Adds nodes to the graph
	public void addNode(int p_id, string p_statement)
	{
		Node temp = new Node(p_id, p_statement);
		nodes.Insert(p_id, temp);
		
		if(!graph.ContainsKey(temp))
			graph.Add(temp, new ArrayList());
		
		if(debug)
				Debug.Log("Added Node: "+p_id);
	}
	
	// Adds response edges to the graph
	public void addEdge(int p_id, string p_statement, int p_fromNode, int p_toNode)
	{
		Response temp = new Response(p_statement, p_fromNode, p_toNode);
		
		edges.Insert(p_id, temp);
		foreach (KeyValuePair<Node, ArrayList> pair in graph)
		{
			if(pair.Key.getId() == p_fromNode)
				pair.Value.Add(temp);
		}
		
		if(debug)
			Debug.Log("Added Response: "+p_id+"\n\tfrom: "+p_fromNode+"\n\tto: "+p_toNode);
	}
	
	// Adds exit edges to the graph
	public void addExit(int p_id, string p_statement, int p_fromNode, int p_toNode)//, int p_altToNode)
	{
		Response temp = new Response(p_statement, p_fromNode, p_toNode);//, p_altToNode);
		temp.setIsExit(true);
		edges.Insert(p_id, temp);
		foreach (KeyValuePair<Node, ArrayList> pair in graph)
		{
			if(pair.Key.getId() == p_fromNode)
				pair.Value.Add(temp);
		}
		
		if(debug)
			Debug.Log("Added Response: "+p_id+"\n\tfrom: "+p_fromNode+"\n\tto: "+p_toNode);//+"\n\tAlternative to: "+p_altToNode);
	}
	
	// Returns the node that matches this ID
	public Node getNode(int p_id)
	{
		if(debug)
			Debug.Log("Getting Node: "+p_id);
		foreach (KeyValuePair<Node, ArrayList> pair in graph)
		{
			if(pair.Key.getId() == p_id)
				return pair.Key;
		}
		return null;
	}
	
	// Returns all the responses for this node
	public ArrayList getResponses(int p_id)
	{
		if(debug)
			Debug.Log("Getting Responses for Node: "+p_id);
		
		foreach (KeyValuePair<Node, ArrayList> pair in graph)
		{
			if(pair.Key.getId() == p_id)
				return pair.Value;
		}
		return null;
	}
	
	// Finds the exit response for this node
	public Node getReentryNode(int p_id)
	{
		if(debug)
			Debug.Log("Getting Exit Response for Node: "+p_id);
		
		foreach (KeyValuePair<Node, ArrayList> pair in graph)
		{
			if(pair.Key.getId() == p_id)
			{
				foreach (Response response in pair.Value)
				{
					if(response.isExit())
					{
						return this.getNode(response.getNextNode());
					}
				}
			}
		}
		return null;
	}
}
