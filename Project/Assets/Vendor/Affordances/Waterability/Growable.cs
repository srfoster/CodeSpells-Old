using UnityEngine;
using System.Collections;

public class Growable : Waterable {
	// Will have to define every time
	public float scaleFactor;
	
	private Vector3 scaleOffset;
	private Vector3 originalScale;
	
	public void Start()
	{
//		Debug.Log("S: Waterable's y position: "+ transform.localPosition.y);
//		Debug.Log("S: Waterable's waterlogAmount: "+ waterlogAmount);
//		Debug.Log("S: Waterable's increaseWaterlogAmount: "+ increaseWaterlogAmount);
//		Debug.Log("S: Waterable's maxWaterlogAmount: "+ maxWaterlogAmount);
//		Debug.Log("S: Waterable's BeingWatered: "+ beingWatered);
//		Debug.Log("S: Waterable's WaterLogged: "+ waterlogged);
		
		originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		scaleOffset = originalScale * scaleFactor;
	}
	
	public override void waterEffectOnObject()
	{
//		Debug.Log("Waterable's watereffect");	
//		Debug.Log("Waterable's waterlogAmount: "+ waterlogAmount);
//		Debug.Log("Waterable's increaseWaterlogAmount: "+ increaseWaterlogAmount);
//		Debug.Log("Waterable's maxWaterlogAmount: "+ maxWaterlogAmount);	
		
		transform.localScale = new Vector3(originalScale.x + scaleOffset.x * waterlogAmount, 
											originalScale.y + scaleOffset.y * waterlogAmount, 
											originalScale.z + scaleOffset.z * waterlogAmount);
	}
}
