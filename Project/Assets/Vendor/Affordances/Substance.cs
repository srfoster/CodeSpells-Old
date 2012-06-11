using UnityEngine;
using System.Collections;

public class Substance : MonoBehaviour {
	private bool is_water = false;
	private bool is_plant = false;
	private bool is_rock = false;

	public bool isWater() {
		return is_water;
	}
	public bool isRock() {
		return is_rock;
	}
	public bool isPlant() {
		return is_plant;
	}
	public void setIsWater()
	{
		is_water = true;	
	}
	public void setIsPlant()
	{
		is_plant = true;	
	}
	public void setIsRock()
	{
		is_rock = true;	
	}
}