using UnityEngine;
using System.Collections;

public class SeedGenerate : MonoBehaviour {
	
	public GameObject red;
	public GameObject blue;
	public GameObject purple;
	public GameObject yellow;
	public GameObject green;
	private ArrayList listOfPrefabs = new ArrayList();
	
	public int number = 5;
	
	void Start () {
		listOfPrefabs.Add(red);
		listOfPrefabs.Add(blue);
		listOfPrefabs.Add(purple);
		listOfPrefabs.Add(yellow);
		listOfPrefabs.Add(green);
		
		foreach (GameObject prefab in listOfPrefabs)
		{
			Debug.Log("On the next kind of flower");
			for(int i = 0; i < number; i++)
			{
				Debug.Log("On the next seed of this kind of flower");
				GameObject obj = Instantiate(prefab, random_location(), Quaternion.identity) as GameObject;
				
				//For variety	
				obj.transform.RotateAround(transform.position, Vector3.up, random_angle()); // Spread them out
				obj.transform.RotateAround(obj.transform.position, Vector3.up, random_angle()); //Spin them a bit
				obj.transform.RotateAround(obj.transform.position, Vector3.left, random_angle()); //Spin them a bit
				obj.transform.RotateAround(obj.transform.position, Vector3.forward, random_angle()); //Spin them a bit
			}
		}
	}
	
	void Update() {
//		foreach (GameObject prefab in listOfPrefabs)
//		{
//			GameObject obj = Instantiate(prefab, random_location(), Quaternion.identity) as GameObject;
//			
//			//For variety	
//			obj.transform.RotateAround(transform.position, Vector3.up, random_angle()); // Spread them out
//			obj.transform.RotateAround(obj.transform.position, Vector3.up, random_angle()); //Spin them a bit
//			obj.transform.RotateAround(obj.transform.position, Vector3.left, random_angle()); //Spin them a bit
//			obj.transform.RotateAround(obj.transform.position, Vector3.forward, random_angle()); //Spin them a bit
//		}
	}
	
	Vector3 random_location()
	{
		float xDistance = Random.Range(-1*transform.localScale.x/2, transform.localScale.x/2);
		float zDistance = Random.Range(-1*transform.localScale.z/2, transform.localScale.z/2);
		
		Vector3 ret = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		ret.x += xDistance;
		ret.z += zDistance;
		ret.y = Terrain.activeTerrain.SampleHeight(ret);
		
		return ret;
	}
	
	float random_angle()
	{
		return 360 * Random.value;
	}
}