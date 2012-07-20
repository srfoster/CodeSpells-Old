using UnityEngine;
using System.Collections;

public class NoUnderground : MonoBehaviour {
	
	public int y_adj = 0;

	void LateUpdate()
	{
		float terrain_height = Terrain.activeTerrain.SampleHeight(transform.position);
		
		if(terrain_height > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, terrain_height + y_adj,transform.position.z);
		}
	}
}
