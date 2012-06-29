using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	public GameObject instantiate(GameObject obj, Vector3 loc, Quaternion rot)
	{
		return Instantiate (obj, loc, rot) as GameObject;	
	}
}
