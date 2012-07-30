using UnityEngine;
using System.Collections;

public class Enchantable : MonoBehaviour {
	
	public GameObject particles;
	public AudioClip audio_clip;
	
	private bool is_enchanted = false;
	private GameObject particle_instance;
	
	private June june;
	
	public delegate void EnchantmentFinishedCallback(GameObject obj);
	
	private EnchantmentFinishedCallback callback;
	
	public delegate void EventHandler(GameObject target, string class_name);
	public static event EventHandler EnchantmentStarted;
	public static event EventHandler EnchantmentEnded;
	public static event EventHandler EnchantmentFailed;
	
	public bool do_restrictions = true;

	
	public string id = "";
		
	public string getId() {
		if (id.Equals("")) {
			return gameObject.GetInstanceID().ToString();
		}
		return id;
	}
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	public void Init(){
		if(id.Equals(""))
		{
			ObjectManager.Register(gameObject);
			//id = gameObject.GetInstanceID().ToString();
		}
		else
			ObjectManager.Register(gameObject,id);
		
		
		
		
		
		
		if(audio_clip == null)
		{
			audio_clip = Resources.Load("SpellEffect") as AudioClip;	
		}
		
		if(particles == null)
		{
			particles = Resources.Load("LightSparkles") as GameObject;	
		}
		
		
		gameObject.AddComponent<Text3D>();
		
		if(do_restrictions)
		{
			gameObject.AddComponent<NoUnderground>();
			gameObject.AddComponent<ScaleLimit>();
		}
		//gameObject.GetComponent<ScaleLimit>().upper_limit = 100;
		//gameObject.GetComponent<ScaleLimit>().lower_limit = 0;
	}
	
	void OnApplicationQuit()
	{
		June.isPlaying = false;	
	}
	
	public void setId(string new_id)
	{
		ObjectManager.Reregister(gameObject,id,new_id);
		
		this.id = new_id;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		gameObject.GetComponent<Text3D>().text = id;

		if(!isEnchanted())
		{
			disenchantAnimation();
			
			if(callback != null)
			{
				callback(gameObject);
				callback = null;
				
				if(june.wasSuccessful())	
				{
					if(EnchantmentEnded != null)
						EnchantmentEnded(gameObject, june.getFileName());
				} else {
					if(EnchantmentFailed != null)
						EnchantmentFailed(gameObject, june.getFileName());
				}
			}
		}
	}
	
	public void setCallback( EnchantmentFinishedCallback callback)
	{
		this.callback = callback;	
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

		is_enchanted = false;
	}
	
	public bool isEnchanted()
	{
		if(june == null)
		{
			return false;	
		}
		
		if(june.isStopped())
			is_enchanted = false;
		
		return is_enchanted;	
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
	
	public string getFileName()
	{
		if(june != null)
			return june.getFileName();	
		
		return "None";
	}
	
	
	//Kind of a bloated, over-worked method.  But I guess I don't fully understand coroutines well enough to modularize it yet.
	private IEnumerator enchantAnimation(){
		
		is_enchanted = true;
	
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
		if(id.Equals(""))
			this.june.setObjectId(gameObject.GetInstanceID().ToString());
		else
			this.june.setObjectId(id);

		this.june.Start();
		
								
		if(EnchantmentStarted != null)
			EnchantmentStarted(gameObject, june.getFileName());
				

		
		
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
