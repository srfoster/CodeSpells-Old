using UnityEngine;
using System.Collections;

public class Level1Flamable : MonoBehaviour {
	public void LateUpdate()
	{
		if(!(this.GetComponent("Waterable") as Waterable).isWaterlogged())
		{
			(this.GetComponent("Flamable") as Flamable).Ignite();
		}
	}	
}