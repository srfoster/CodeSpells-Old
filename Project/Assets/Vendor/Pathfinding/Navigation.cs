using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Navigation : MonoBehaviour {
	private List<Vector3> movements;
	private Vertex endVertex = null;
	private Vertex startVertex = null;
	private bool has_started = false;
	

	public List<Vector3> findPath(Vector3 start, Vector3 end)
	{		
		List<Vertex> allVertices = buildGraph(start, end);//start and end
		
		List<Vertex> priorityVertices = new List<Vertex>();
		Vertex currentVertex = null;
		
		startVertex.distTo = 0;
		priorityVertices.Add (startVertex);
		while (priorityVertices.Count != 0) { //pop off the element with the smallest priority
			currentVertex = priorityVertices[0];
			priorityVertices.RemoveAt(0);
			
			foreach (Vertex vert in currentVertex.adj) { //get all adjacent vertices
				if (!vert.hasTraversed) {					//distance along terrain I can make more advanced,
															//In the future, I could precompute all the distances between
															//adjacent waypoints and store them at the start or index them
					if (vert.distTo > currentVertex.distTo + Vector3.Distance(currentVertex.myvector, vert.myvector)) {
						vert.distTo = currentVertex.distTo + Vector3.Distance(currentVertex.myvector, vert.myvector);
						vert.prev = currentVertex;
						priorityVertices = enqueue(priorityVertices, vert); //add to queue
					}
				}
			}
			currentVertex.hasTraversed = true;
		}
		return finalPath (endVertex);
	}
	
	private List<Vector3> finalPath(Vertex currentVertex) { 

		List<Vertex> path = new List<Vertex>();
		while(currentVertex.prev != null) {
			path.Add (currentVertex);
			currentVertex = currentVertex.prev;
		}
		path.Reverse ();
		
		List<Vector3> positions = new List<Vector3>();
		foreach(Vertex vert in path) {
			positions.Add (vert.myvector);
		}
		return positions;
	}
	
	//adds the vertex to the right place in the priority queue
	private List<Vertex> enqueue (List<Vertex> lv, Vertex vert) {
		int counter = 0;
		foreach (Vertex v in lv) {
			if (v.distTo > vert.distTo) {
				lv.Insert (counter, vert);
				return lv;
			}
			counter++;
		}
		lv.Add(vert);
		return lv;
	}
	
	private List<Vertex> buildGraph(Vector3 start, Vector3 end) { //I can build the graph once in the future
		
		List<Vertex> allVertices = new List<Vertex>();
		int counter = 0;
		double connectToStartDist = Double.MaxValue;
		double connectToEndDist = Double.MaxValue;
		Vertex connectToStart = null;
		Vertex connectToEnd = null;
			
		foreach (Transform t in transform) {
			Waypoint w = t.gameObject.GetComponent<Waypoint>();
			
			Vertex temp = new Vertex(w.location());
			allVertices.Add (temp);
			if(Vector3.Distance(start, w.location ()) < connectToStartDist) {
				connectToStartDist = Vector3.Distance(start, w.location ());
				connectToStart = temp;
			}
			if(Vector3.Distance(end, w.location ()) < connectToEndDist) {
				connectToEndDist = Vector3.Distance(end, w.location ());
				connectToEnd = temp;
			}
		}
		//allVertices has all vertices in the graph
		foreach (Transform t in transform) { //for each waypoint
			Waypoint w = t.gameObject.GetComponent<Waypoint>();

			foreach (Vector3 tempAdj in w.getAdjacent()) { //for each adjacent Vector3
				
				foreach (Vertex vert in allVertices) {
					if (vert.isSameLocation(tempAdj)) {
						allVertices[counter].addAdjVert(vert);
						break;
					}
				}
			}
			counter++;
		}
		startVertex = new Vertex(start);
		endVertex = new Vertex(end);
		startVertex.addAdjVert(connectToStart);
		connectToEnd.addAdjVert(endVertex);

		allVertices.Add(startVertex);
		allVertices.Add(endVertex);
		
		return allVertices;
	}
	
	private class Vertex {
		public Vertex (Vector3 v) {
			myvector = v;
			hasTraversed = false;
			distTo = Double.MaxValue;
		}
		public Vector3 myvector;
		public double distTo;
		public bool hasTraversed;
		public List<Vertex> adj = new List<Vertex>();
		public Vertex prev = null;
		
		public void addAdjVert(Vertex v) {
			adj.Add (v);	
		}
		
		public bool isSameLocation(Vector3 v) {
			return v == myvector;	
		}
	}
}
