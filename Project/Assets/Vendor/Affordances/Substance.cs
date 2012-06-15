using UnityEngine;
using System.Collections;

public class Substance : MonoBehaviour {
	public bool is_rock = false;
	public bool is_wood = false;
	public bool is_water = false;
	public bool is_plant = false;
	public bool is_seed = false;

	public bool isRock()
	{
		return is_rock;	
	}
	
	public bool isWood()
	{
		return is_wood;	
	}
	
	public bool isWater()
	{
		return is_water;	
	}
	
	public bool isPlant()
	{
		return is_plant;	
	}

	public bool isSeed()
	{
		return is_seed;	
	}
}