using UnityEngine;
using System.Collections;

public class ProcGenerate : MonoBehaviour {
	
	public GameObject prefab;
	
	public int number = 10;
	
	
	void Start () {
		for(int i = 0; i < number; i++)
		{
			GameObject obj = Instantiate(prefab, random_location(), Quaternion.identity) as GameObject;
			
			//For variety	
			obj.transform.RotateAround(transform.position, Vector3.up, random_angle()); // Spread them out
			obj.transform.RotateAround(obj.transform.position, Vector3.up, random_angle()); //Spin them a bit
			obj.transform.RotateAround(obj.transform.position, Vector3.left, random_angle()); //Spin them a bit
			obj.transform.RotateAround(obj.transform.position, Vector3.forward, random_angle()); //Spin them a bit
			
			//Fix height
		//	adjust_to_terrain(obj);
		}
	}
	
	void Update() {
		
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
	
	void adjust_to_terrain(GameObject obj)
	{
		Vector3 old_pos = obj.transform.position;
		
		float yadj = Terrain.activeTerrain.SampleHeight(old_pos);
		yadj = obj.transform.localScale.y * Random.value; //Some height variation
		
		Vector3 new_pos = new Vector3(old_pos.x, yadj, old_pos.z);
		
		obj.transform.position = new_pos;
	}
}
