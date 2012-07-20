using UnityEngine;
using System.Collections;

public class NoUnderground : MonoBehaviour {

	void LateUpdate()
	{
		float terrain_height = Terrain.activeTerrain.SampleHeight(transform.position);
		
		if(terrain_height > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, terrain_height,transform.position.z);
		}
	}
}
