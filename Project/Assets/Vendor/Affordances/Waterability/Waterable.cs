using UnityEngine;
using System.Collections;

public abstract class Waterable : MonoBehaviour {
	// Most likely will not change
	public float waterlogAmount = 0.0f;
	public bool beingWatered = false;
	public float decreaseWaterlogAmount = 0;
	
	// Will have to define each time
	public float increaseWaterlogAmount;
	
	// Will never change
	private static float maxWaterlogAmount = 1.0f;
	
	// When water hits a waterable object, it is being watered
	public void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.GetComponent<Substance>() != null && collider.gameObject.GetComponent<Substance>().isWater())
		{
			beingWatered = true;
		}
	}
	
	// If a waterable object is being watered, it should become more waterlogged
	public void Update()
	{
		onUpdate();
	}
	
	public bool isHalfWaterlogged()
	{
		if(waterlogAmount >= (maxWaterlogAmount/2))
			return true;
		
		return false;
	}
	
	public bool isWaterlogged()
	{
		if(waterlogAmount >= maxWaterlogAmount)
			return true;
		
		return false;
	}
	
	// When water stops hitting a waterable object, it is no longer being watered
	void OnTriggerExit(Collider collider)
	{
		if(collider.gameObject.GetComponent<Substance>() != null && collider.gameObject.GetComponent<Substance>().isWater())
		{
			beingWatered = false;
		}
	}
	
	public abstract void waterEffectOnObject();
	
	public virtual void onUpdate()
	{
		//Debug.Log("For this object: "+transform.name +" the waterlogAmount is: "+waterlogAmount);
		//Debug.Log("This object: "+transform.name +" is waterlogged: "+isWaterlogged());
		if(beingWatered && !isWaterlogged())
		{
			waterEffectOnObject();
			waterlogAmount += increaseWaterlogAmount;
		}
		if(!beingWatered && waterlogAmount > 0)
		{
			waterlogAmount -= decreaseWaterlogAmount;
		}
	}
}