using UnityEngine;
using System.Collections;

public class ProcGenerate : MonoBehaviour {
	
	public GameObject prefab;
	
	public int number = 20;
	
	void Start () {
		for(int i = 0; i < number; i++)
		{
			GameObject obj = Instantiate(prefab, random_location(), Quaternion.identity) as GameObject;
			
			//For variety	
			obj.transform.RotateAround(transform.position, Vector3.up, random_angle()); // Spread them out
			obj.transform.RotateAround(obj.transform.position, Vector3.up, random_angle()); //Spin them a bit
			obj.transform.RotateAround(obj.transform.position, Vector3.left, random_angle()); //Spin them a bit
			obj.transform.RotateAround(obj.transform.position, Vector3.forward, random_angle()); //Spin them a bit
		}
	}
	
	Vector3 random_location()
	{
		float distance = Random.Range(0.0f, transform.localScale.x);
		
		
		Vector3 ret = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		ret.x += distance;
		
		ret.y = Terrain.activeTerrain.SampleHeight(ret);
		
		return ret;
	}
	
	float random_angle()
	{
		return 360 * Random.value;
	}
}