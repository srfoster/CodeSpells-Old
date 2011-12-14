/* See the copyright information at https://github.com/srfoster/Inventory/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public class SpellItem : DraggableItem
{

	public AudioSource audio_source;
	public GameObject particles;
	public AudioClip audio_clip;

	override public void DroppedOn(GameObject target)
	{
		if(target != null)
		{
			getInventory().removeItem(gameObject);
			magicAnimation(target);
		}
		else
		{
			SetHidden(false);
		}
	}

	/*
		An example magic animation.
	*/
	void magicAnimation (GameObject target) {
	
		GameObject obj = Instantiate(particles,target.transform.position, target.transform.rotation) as GameObject;
		obj.transform.parent = transform;
		
		audio_source.audio.clip = audio_clip;
		audio_source.audio.Play();
	
		//Quick hack to make the sun go dark
		/*
		var f = function() : IEnumerator {
			if(GameObject.Find("Sun") != null)
			{
				while(GameObject.Find("Sun").light.intensity > 0)
				{
					GameObject.Find("Sun").light.intensity -= 0.01;
					yield;
				}
			}
		};
	
		getInventory().StartCoroutine(f());
		*/
	
		target.animation.Play(target.animation.clip.name);
		
		//yield WaitForSeconds(2);
		
		//Restore the sun
		/*
		f = function() : IEnumerator {
			if(GameObject.Find("Sun") != null)
			{
				while(GameObject.Find("Sun").light.intensity < 0.5)
				{
					GameObject.Find("Sun").light.intensity += 0.01;
					yield;
				}
			}
		};
		
		getInventory().StartCoroutine(f());
		*/
	}

}
