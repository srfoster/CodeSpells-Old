using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {
	private bool is_rock = false;
	private bool is_plant = false;
	//if false it is a plant, if true it is a rock

	public bool isRock()
	{
		return is_rock;	
	}
	
	public void setIsRock()
	{
		is_rock = true;	
	}
	
	public bool isPlant()
	{
		return is_plant;	
	}
	
	public void setIsPlant()
	{
		is_plant = true;	
	}
}