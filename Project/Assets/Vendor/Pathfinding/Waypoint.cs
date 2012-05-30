using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
	
	
	private ArrayList adjacent;
	
	void Start()
	{
		adjacent = new ArrayList();
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if(col.gameObject.GetComponent<Waypoint>() == null)
			return;
		
		Vector3 waypoint_location = col.gameObject.transform.position;
		waypoint_location.y = Terrain.activeTerrain.SampleHeight(waypoint_location);
		
		if(!adjacent.Contains(waypoint_location)) {
			//Debug.Log ("adding adjacent");
			adjacent.Add(waypoint_location);
		}
	}
	
	public ArrayList getAdjacent()
	{
		return adjacent;	
	}
	
	public string print()
	{
		string ret = "";
		ret +="MyLocation: "+ location().x + "\nAdjacency List:\n";
		foreach(Vector3 vector in adjacent)
		{
			ret += vector.x+"\n";	
		}
		return ret;
	}
	
	public Vector3 location()
	{
		Vector3 my_location = transform.position;
		my_location.y = Terrain.activeTerrain.SampleHeight(my_location);
		
		return my_location;
	}
	
}
