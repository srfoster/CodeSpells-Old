using UnityEngine;
using System.Collections;

public class Flamable : MonoBehaviour {
	
	public bool flaming_at_start = false;
	
	private GameObject flames_actual;
	Texture2D old_texture;
	
	public delegate void EventHandler(GameObject target);
	public static event EventHandler CaughtFire;
	public static event EventHandler Extinguished;
	
	void Start()
	{
		if(flaming_at_start)
		{
			Ignite();	
		}
	}

	public void Ignite()
	{
		if(isIgnited())
			return;
		
		if(CaughtFire != null)
			CaughtFire(this.gameObject);
		
		//look at collider dimensions
		if ((transform.collider.bounds.size.x > 10) || (transform.collider.bounds.size.y > 10) || (transform.collider.bounds.size.z > 10)) {
			flames_actual = Instantiate(Resources.Load ("TallFire"), transform.position, Quaternion.identity) as GameObject;
		}
		else if ((transform.collider.bounds.size.x > 3) || (transform.collider.bounds.size.y > 3) || (transform.collider.bounds.size.z > 3)) {
			flames_actual = Instantiate(Resources.Load ("MedFire"), transform.position, Quaternion.identity) as GameObject;
		}
		else {
			flames_actual = Instantiate(Resources.Load ("Fire"), transform.position, Quaternion.identity) as GameObject;			
		}
		flames_actual.transform.parent = transform;
		
		if(gameObject.GetComponent<Item>() != null)
		{
			old_texture = gameObject.GetComponent<Item>().inventoryTexture;
			try{
				gameObject.GetComponent<Item>().inventoryTexture = Resources.Load("flaming_"+old_texture.name) as Texture2D;
			}catch{
				
			}
		}
	}
	
	public bool isIgnited()
	{
		return flames_actual != null;
	}
	
	public void Extinguish()
	{
		if(!isIgnited())
			return;
		
		Destroy(flames_actual);
		
		if(gameObject.GetComponent<Item>() != null && old_texture != null)
		{
			try{
				gameObject.GetComponent<Item>().inventoryTexture = old_texture;
			}catch{
				
			}
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		DetermineIgniteOrExtinguish(col.gameObject);
	}
		
	void OnTriggerEnter(Collider col)
	{
		DetermineIgniteOrExtinguish(col.gameObject);
	}
	
	void DetermineIgniteOrExtinguish(GameObject col)
	{
		if(col.GetComponent<Substance>() != null && col.GetComponent<Substance>().isWater())
		{
			if(Extinguished != null)
				Extinguished(this.gameObject);
			
			Extinguish();
		}
		if(col.GetComponent("Flamable") == null)
		{
			return;	
		}
		
		Flamable other_flamable = col.GetComponent("Flamable") as Flamable;
		
		if(other_flamable.isIgnited())
			Ignite();
	}
}