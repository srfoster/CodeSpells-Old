using UnityEngine;
using System.Collections;

public class Enchantable : MonoBehaviour {
	
	public GameObject particles;
	public AudioClip audio_clip;
	public AudioSource audio_source;
	
	private bool is_enchanted;
	private GameObject particle_instance;
	
	private June june;
	
	public delegate void EnchantmentFinishedCallback(GameObject obj);
	
	private EnchantmentFinishedCallback callback;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(june != null && june.isStopped())
		{
			disenchantAnimation();
			
			if(callback != null)
			{
				callback(gameObject);
				callback = null;
			}
		}
	}
	
	public void enchant(June june, EnchantmentFinishedCallback callback)
	{
		this.june = june;
		this.callback = callback;
		
		this.june.Start();
		
		enchantAnimation();
	}
	
	public June getJune()
	{
		return this.june;
	}
	
	public void disenchant(){
		if(isEnchanted())
			june.Stop();
	}
	
	public bool isEnchanted()
	{
		return june != null;	
	}
	
	private void disenchantAnimation()
	{
		Destroy(particle_instance);
		
		//For disenchanting levitations.
		if(rigidbody != null)
		{
			rigidbody.useGravity = true;	
		}
	}
	
	private void enchantAnimation(){
		particle_instance = Instantiate(particles,transform.position, Quaternion.AngleAxis(0, new Vector3(0,1,0))) as GameObject;
		particle_instance.transform.parent = transform;
		
		if(audio_source != null && audio_source.audio != null && audio_clip != null)
		{
			audio_source.audio.clip = audio_clip;
			audio_source.audio.Play();
		}

		if(animation != null && animation.clip != null)
			animation.Play(animation.clip.name);
	}
}
