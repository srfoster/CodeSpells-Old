using UnityEngine;
using System.Collections;

public class CreateTerrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TerrainData terrainData = Terrain.activeTerrain.terrainData;
		
		float[,] newSamples = new float[terrainData.heightmapHeight, terrainData.heightmapWidth];

        for (int x = 0; x < terrainData.heightmapWidth; x++)
		{
            for (int y = 0; y < terrainData.heightmapHeight; y++)
            {
                newSamples[y,x] = (float)x / terrainData.heightmapWidth;
            }
        }

    	terrainData.SetHeights(0, 0, newSamples);
	}
}