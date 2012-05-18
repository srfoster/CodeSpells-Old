using UnityEngine;
using System.Collections;

public class Fillable : Waterable {
	// Will have to define every time
	public float maxYOffset;
	
	private float originalY;
	
	public void Start()
	{
//		Debug.Log("S: Fillable's y position: "+ transform.localPosition.y);
//		Debug.Log("S: Fillable's waterlogAmount: "+ waterlogAmount);
//		Debug.Log("S: Fillable's increaseWaterlogAmount: "+ increaseWaterlogAmount);
//		Debug.Log("S: Fillable's maxWaterlogAmount: "+ maxWaterlogAmount);
//		Debug.Log("S: BeingWatered: "+ beingWatered);
//		Debug.Log("S: WaterLogged: "+ waterlogged);
	
		originalY = transform.localPosition.y;
	}
	
	public override void waterEffectOnObject()
	{
//		Debug.Log("Fillable's watereffect");	
//		Debug.Log("Fillable's waterlogAmount: "+ waterlogAmount);
//		Debug.Log("Fillable's increaseWaterlogAmount: "+ increaseWaterlogAmount);
//		Debug.Log("Fillable's maxWaterlogAmount: "+ maxWaterlogAmount);
		
		transform.localPosition = new Vector3(transform.localPosition.x, 
											originalY + maxYOffset * waterlogAmount, 
											transform.localPosition.z);	
	}
}
