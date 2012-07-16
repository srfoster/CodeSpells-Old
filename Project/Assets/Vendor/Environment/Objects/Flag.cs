using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flag : PickUpableItem {
	
	private static List<int> num_list = new List<int>();
	
	public override void DroppedOn(GameObject target)
	{
		GameObject spawningZone = GameObject.Find ("Spawning Zone");
		Vector3 nextPosition = new Vector3(spawningZone.transform.position.x, Terrain.activeTerrain.SampleHeight(spawningZone.transform.position),spawningZone.transform.position.z);
		GameObject game_flag = Instantiate(Resources.Load("Flag") as GameObject, nextPosition, Quaternion.identity) as GameObject;

		int num = getNextNumber();
		game_flag.GetComponent<Enchantable>().setId("Area "+num);
		num_list.Add(num);	
		
		SetHidden(false);
	}
	
	private int getNextNumber()
	{
		if(num_list.Count == 0)
			return 1;
		
		int max = 0;
		foreach(int i in num_list)
		{
			if(i > max)
				max = i;
		}
		
		for(int i = 1; i < max; i++)
		{
			if(!num_list.Contains(i))
				return i;
		}
		
		return max + 1;
	}
	
	override public void handleMouseDown() {
		
		int to_remove = int.Parse(gameObject.GetComponent<Enchantable>().getId().Split(' ')[1]);
		
		num_list.Remove (to_remove);
		Destroy (gameObject);
	}
	
	override public Texture2D getTexture()	
	{
		Texture2D flagImg = Resources.Load("flag_image") as Texture2D;
		return flagImg;
		
	}
}
