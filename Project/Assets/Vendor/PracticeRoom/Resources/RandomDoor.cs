using UnityEngine;
using System.Collections;

public class RandomDoor : MonoBehaviour {

	public void changeDoor()
	{
		Debug.Log("Oh Em to the G....about to change it");
		System.Random random = new System.Random();
		int randomNumber = random.Next(1, 3);
		Debug.Log("I chose a rnadom number!!!!! It was: "+randomNumber);
		
		if(randomNumber == 1)
		{
			Debug.Log("So I should be in here making the left wall disappear...what what!");
			transform.Find("LeftWall").gameObject.active = false;
			transform.Find("RightWall").gameObject.active = true;
		}
		else
		{
			Debug.Log("So I should be in here making the right wall disappear...oh no she didn't!");
			transform.Find("RightWall").gameObject.active = false;
			transform.Find("LeftWall").gameObject.active = true;
		}
		Debug.Log("Done changing Biatches");
	}
}
