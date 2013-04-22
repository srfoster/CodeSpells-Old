using UnityEngine;
using System.Collections;

public class Growable : Waterable {
	// Will have to define every time
	public float scaleFactor = 1;
	
	private Vector3 scaleOffset;
	private Vector3 originalScale;
	
	public void Start()
	{
		originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		scaleOffset = originalScale * scaleFactor;
		Debug.Log("For this object: "+transform.name +" the original scale is: "+originalScale);
	}
	
	public override void waterEffectOnObject()
	{
		transform.localScale = new Vector3(originalScale.x + scaleOffset.x * waterlogAmount, 
											originalScale.y + scaleOffset.y * waterlogAmount, 
											originalScale.z + scaleOffset.z * waterlogAmount);
	}
}