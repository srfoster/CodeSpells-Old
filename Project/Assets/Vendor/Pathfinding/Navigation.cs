using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {
	/*
	void OnGUI() {
	    if (GUI.Button(new Rect(10,10,100,100),"Path find test"))
		{
			findPath(GameObject.Find("First Person Controller").transform.position, GameObject.Find("rock").transform.position);	
		}
	}
	
	public ArrayList findPath(Vector3 start, Vector3 end)
	{		
		Vector3 waypoint_start = findClosest(start);
		
		Vector3 waypoint_end   = findClosest(end);
		
		ArrayList edges = getEdges();
		
		ArrayList edge1 = new ArrayList();
		edge1.Add(start,end);
		edges.add(edge1);
		
		ArrayList edge2 = new ArrayList();
		edge2.Add(start,waypoint_start);
		edges.add(edge2);
		
		ArrayList edge3 = new ArrayList();
		edge3.Add(waypoint_end,end);
		edges.add(edge3);
		
		ArrayList verts = getVerts();
		verts.Add(start);
		verts.Add(end);
		
		return shortestPath(edges, verts, start, end);
	}
	
	public ArrayList shortestPath(ArrayList edges, ArrayList verts, Vector3 start, Vector3 end)
	{
		ArrayList explored = new ArrayList();
		explored.Add(start);
		
		Hashtable distances = new Hashtable();
		distances.Add(start,0);
		
		while(explored.Count < verts.Count)
		{
			
			//Update distances
			foreach(Vector3 v in verts)
			{
				foreach(Vector3 u in explored)
				{
					ArrayList edge = new ArrayList();
					edge.Add(u,v);
					
					if(edges.Contains(edge)) //Probably won't work
					{
						if(distances[v] == null || Vector3.Distance(u,v) < distances[v])
						{
							distances[v] = Vector3.Distance(u,v) < distances[v];
						}
					}
				}
			}
			
			//Pick closest
			Vector3 closest = new Vector3();
			
			foreach(DictionaryEntry de in distances)
			{
				if(distances[closest] == null || distances[closest]  < de.Value)
				{
					closest = de.Key;	
				}
			}
		}
	}
	
	public Vector3 findClosest(Vector3 v)
	{
		Vector3 closest = new Vector3();
		

		foreach(Transform child in transform)
		{
			Waypoint w = child.gameObject.GetComponent<Waypoint>();
			
			if(w == null)
				continue;

			if(Vector3.Distance(w.location(), v) < Vector3.Distance(closest,v))
				closest = w.location();
			
			
		}
		
		return closest;
	}
	
	public ArrayList getVerts()
	{
		ArrayList ret = new ArrayList();
		
		foreach(Transform child in transform)
		{
			Waypoint w = child.gameObject.GetComponent<Waypoint>();
			
			if(w == null)
				continue;
			
			ret.Add(w.location());
		}
		
		return ret;
	}
	
	public ArrayList findWaypointPath()
	{
		ArrayList edges = new ArrayList();
		
		//Build the graph
		foreach(Transform child in transform)
		{
			Waypoint w = child.gameObject.GetComponent<Waypoint>();
			
			if(w == null)
				continue;
			
			foreach(Vector3 v in w.getAdjacent())
			{
				ArrayList edge1 = new ArrayList();
				
				edge1.Add(w.location());
				edge1.Add(v);
				
				edges.Add(edge1);
				
			}
		}
		
		
		return edges;
	}
	*/
}
