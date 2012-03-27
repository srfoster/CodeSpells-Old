using UnityEngine;
using System.Collections;

public class Node {
	int id;
	string statement = null;
	
	public Node (int p_id, string p_statement)
	{
		id = p_id;
		statement = p_statement;
	}

	public string getStatement()
	{
		return statement;	
	}
	
	public int getId()
	{
		return id;	
	}
}
