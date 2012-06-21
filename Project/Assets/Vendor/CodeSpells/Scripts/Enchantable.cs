using UnityEngine;
using System.Collections;

public class Enchantable : MonoBehaviour {
	
	public GameObject particles;
	public AudioClip audio_clip;
	
	private bool is_enchanted;
	private GameObject particle_instance;
	
	private June june;
	
	public delegate void EnchantmentFinishedCallback(GameObject obj);
	
	private EnchantmentFinishedCallback callback;
	
	// Use this for initialization
	void Start () {
		ObjectManager.Register(gameObject);
		
		if(audio_clip == null)
		{
			audio_clip = Resources.Load("SpellEffect") as AudioClip;	
		}
		
		if(particles == null)
		{
			particles = Resources.Load("LightSparkles") as GameObject;	
		}
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
		
		StartCoroutine(enchantAnimation());
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
	
	
	//Kind of a bloated, over-worked method.  But I guess I don't fully understand coroutines well enough to modularize it yet.
	private IEnumerator enchantAnimation(){
	
		//Fade the light out
		if(GameObject.Find("Directional light") != null)
		{
			while(GameObject.Find("Directional light").light.intensity > 0)
			{
				GameObject.Find("Directional light").light.intensity -= 0.1f;
				yield return null;
			}
		}	

		//Make spakles
		particle_instance = Instantiate(particles,transform.position, Quaternion.AngleAxis(0, new Vector3(0,1,0))) as GameObject;
		particle_instance.transform.parent = transform;
		
		
		//Play the sound effect.
		AudioSource audio_source = GameObject.Find("Voice").audio;
		
		if(audio_clip != null)
		{
			audio_source.audio.clip = audio_clip;
			audio_source.audio.Play();
		}
		
		//Starts the external Java program (the most important line of the method)
		this.june.Start();
		
		//If the enchantable object has an animation (i.e. the broom) play it.
		if(animation != null && animation.clip != null)
			animation.Play(animation.clip.name);
		
		yield return new WaitForSeconds(2);
		
		//Fade the light back in.
		if(GameObject.Find("Directional light") != null)
		{
			while(GameObject.Find("Directional light").light.intensity < 0.5f)
			{
				GameObject.Find("Directional light").light.intensity += 0.1f;
				yield return null;
			}
		}	
	}

}
