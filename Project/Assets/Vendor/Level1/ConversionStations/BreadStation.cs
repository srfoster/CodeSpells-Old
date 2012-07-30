using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreadStation : MonoBehaviour {
	
	//store an array of bread game objects
	public GameObject bread;
	private List<GameObject> breadList = new List<GameObject>();
	private float breadOffset = 1.0f;
	
	public void addBread() {
		float randRadius;
		float randAngle;
		float xDistance;
		float zDistance;
		Vector3 breadPos;
		GameObject actBread;
		
		for(int i = 0; i < 10; i++)
		{
			randRadius = Random.Range (0f, (float)(transform.localScale.x/2-breadOffset));
			randAngle = Random.Range (0f, 2*Mathf.PI);
			xDistance = transform.position.x + randRadius*Mathf.Cos (randAngle);
			zDistance = transform.position.z + randRadius*Mathf.Sin (randAngle);
			
			breadPos = new Vector3(xDistance, transform.position.y, zDistance);
			
			actBread = (Instantiate(bread, breadPos, Quaternion.identity) as GameObject);
			breadList.Add(actBread);
		}
	}
	
	public bool isEmpty() {
		return (breadList.Count == 0);
	}
	
	public GameObject getBread () {
		GameObject tempBread = breadList[0];
		breadList.RemoveAt(0);
		return tempBread;
	}
}
