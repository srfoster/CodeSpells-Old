using UnityEngine;
using System.Collections;

public class PickUpableItem : DraggableItem {
	
	public override void DroppedOn(GameObject target)
	{	
		//get spawning zone
		if (transform.gameObject.GetComponent<NoUnderground>() == null) {
			if (nucomp)
				nucomp.enabled = true;
		}
		GameObject spawningZone = GameObject.Find ("Spawning Zone");
		Vector3 nextPosition = new Vector3(spawningZone.transform.position.x, Terrain.activeTerrain.SampleHeight(spawningZone.transform.position),spawningZone.transform.position.z);
		transform.position = nextPosition;
		getInventory().removeItem(gameObject);
		SetHidden(false);	
	}
	
	public override void DroppedOnInventory(Vector3 mousePosition)
	{
		SetHidden(false);	
	}
}
