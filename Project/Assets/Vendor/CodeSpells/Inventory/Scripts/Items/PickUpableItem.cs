using UnityEngine;
using System.Collections;

public class PickUpableItem : DraggableItem {
	
	public override void DroppedOn(GameObject target)
	{	
		//get spawning zone
		GameObject spawningZone = GameObject.Find ("Spawning Zone");
		getInventory().removeItem(transform.gameObject);
		Vector3 nextPosition = new Vector3(spawningZone.transform.position.x, Terrain.activeTerrain.SampleHeight(spawningZone.transform.position),spawningZone.transform.position.z);
		transform.position = nextPosition;
	}
}
