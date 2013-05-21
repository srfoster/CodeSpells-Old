using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enchantable : MonoBehaviour {
	
	public GameObject particles;
	public AudioClip audio_clip;
	
	private bool is_enchanted = false;
	private bool enchantmentEnded = false;
	private bool newEnchantment = true;
	private Stack enchantmentsRunning;
	private GameObject particle_instance;
	
	private June june;
	private Stack junes;
	
	public delegate void EnchantmentFinishedCallback(GameObject obj);
	
	private EnchantmentFinishedCallback callback;
	private Stack callbacks;
	
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
		
		
		
		junes = new Stack();
		enchantmentsRunning = new Stack();
		callbacks = new Stack();
		
		
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
			//gameObject.AddComponent<ScaleLimit>();
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
		
		if(gameObject.GetComponent<Text3D>() != null)
		{	
			gameObject.GetComponent<Text3D>().text = id;
		}
		
		////////////////////////////////
// 		if (june.isStopped()) {
// 		    if (!isEnchanted()) {
// 		        disenchantAnimation();
// 		    }
// 		    if (callback != null) {
// 		        callback(gameObject);
// 		        if (!isEnchanted())
// 		            callback = null;
// 		        if (june.wasSuccessful()) {
// 		            if(EnchantmentEnded != null)
// 						EnchantmentEnded(gameObject, june.getFileName());
// 				} else {
// 					if(EnchantmentFailed != null)
// 						EnchantmentFailed(gameObject, june.getFileName());
// 				}
// 		    }
// 		}
		////////////////////////////////
		
		if(june != null && !isEnchanted())
		{
		    //ProgramLogger.LogKV("enchantments", hasRunningEnchantment()+"");
		    if (!hasRunningEnchantment())
			    disenchantAnimation();
			
			if(callback != null)
			{
				callback(gameObject);
				//if (!hasRunningEnchantment())
				//    callback = null;
				
				if(june.wasSuccessful())	
				{
					if(EnchantmentEnded != null)
						EnchantmentEnded(gameObject, june.getFileName());
				} else {

					if(EnchantmentFailed != null)
					{
						EnchantmentFailed(gameObject, june.getFileName());
					}
				}
			}
			
			junes.Pop();
			enchantmentsRunning.Pop();
			callbacks.Pop();
			if (junes.Count > 0) {
			    june = (June) junes.Peek();
			    is_enchanted = (bool) enchantmentsRunning.Peek();
			    callback = (EnchantmentFinishedCallback) callbacks.Peek();
			} else {
			    june = null;
			    is_enchanted = false;
			    callback = null;
			    newEnchantment = true;
			}
		}
		
// 		if(!isEnchanted())
// 		{
// 			disenchantAnimation();
// 			
// 			if(callback != null)
// 			{
// 				callback(gameObject);
// 				callback = null;
// 				
// 				if(june.wasSuccessful())	
// 				{
// 					if(EnchantmentEnded != null)
// 						EnchantmentEnded(gameObject, june.getFileName());
// 				} else {
// 
// 					if(EnchantmentFailed != null)
// 					{
// 						EnchantmentFailed(gameObject, june.getFileName());
// 					}
// 				}
// 			}
// 		}
	}
	
	public void setCallback( EnchantmentFinishedCallback callback)
	{
		this.callback = callback;	
	}
	
	public void enchant(June june, EnchantmentFinishedCallback callback)
	{	
				
		this.june = june;
		this.callback = callback;
		this.junes.Push(june);
		this.enchantmentsRunning.Push(true);
		this.callbacks.Push(callback);
		
		StartCoroutine(enchantAnimation());
	}
	
	public June getJune()
	{
	    //return this.junes.Peek();
		return this.june;
	}
	
	public void stopAll() {
	    foreach (June j in junes)
	        j.Stop();
	}
	
	public string disenchant(){
		if(isEnchanted()) {
			june.Stop();
		}

		is_enchanted = false;
        enchantmentsRunning.Pop();
        enchantmentsRunning.Push(false);
		
		return getFileName();
	}
	
	// This is about the most horrible way to do this, but Stacks don't allow me to remove an arbirtrary element. I mean, seriously?
	public void disenchant(string name) {
	    Stack tempj = new Stack();
	    Stack tempe = new Stack();
	    Stack tempc = new Stack();
	    June thejune;
	    int count = junes.Count;
	    bool found = false;
	    // Lock the stacks so that no one else modifies them
	    lock (junes.SyncRoot) {
	    lock (enchantmentsRunning.SyncRoot) {
	    lock (callbacks.SyncRoot) {
	        // Find the right june, pushing non-matching ones onto temporary stacks
	        for (int i=0; i<count; i++) {
	            thejune = (June) junes.Pop();
	            if (thejune.getFileName().Equals(name)) {
	                june = thejune;
	                enchantmentsRunning.Pop();
	                is_enchanted = false;
	                callback = (EnchantmentFinishedCallback) callbacks.Pop();
	                found = true;
	                break;
	            }
	            tempj.Push(thejune);
	            tempe.Push(enchantmentsRunning.Pop());
	            tempc.Push(callbacks.Pop());
	        }
	        // Put items on the temporary stacks back in place
	        count = tempj.Count;
	        for (int i=0; i<count; i++) {
	            junes.Push(tempj.Pop());
	            enchantmentsRunning.Push(tempe.Pop());
	            callbacks.Push(tempc.Pop());
	        }
	        if (found) {
                // Put the matching june at the top of the stack
                junes.Push(june);
                enchantmentsRunning.Push(is_enchanted);
                callbacks.Push(callback);
	        }
	    }}}
	    if (found)
	        june.Stop();
	}
	
	// Returns true if the most recently cast spell is running, false if it is not running
	//  Note that other spells may also be running on this object regardless of what is returned here
	public bool isEnchanted()
	{
	////////////////////////////////
// 	    if (junes.Count == 0)
// 	        return false;
// 	    
// 	    foreach (June j in junes) {
// 	        if (!j.isStopped())
// 	            return true;
// 	    }
// 	    return false;
	    /////////////////////////////////
	    
		if(june == null)
		{
			//return true;	
			return false;
		}
		
		if(june.isStopped()) {
			is_enchanted = false;
			enchantmentsRunning.Pop();
			enchantmentsRunning.Push(false);
		}
		
		return is_enchanted;	
	}
	
	// Returns true if at least one spell is still running on this object
	public bool hasRunningEnchantment()
	{
	    if (junes.Count == 0)
	        return false;
	    return enchantmentsRunning.Contains(true);
// 	    foreach (June j in junes) {
// 	        if (!j.isStopped())
// 	            return true;
// 	    }
// 	    return false;
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
	
	public List<string> getFileNames()
	{
	    List<string> names = new List<string>();
	    if (junes.Count == 0) {
	        names.Add("None");
	        return names;
	    }
	    foreach (June j in junes) {
	        names.Add(j.getFileName());
	    }
	    return names;
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
		if (newEnchantment) {
            particle_instance = Instantiate(particles,transform.position, Quaternion.AngleAxis(0, new Vector3(0,1,0))) as GameObject;
            particle_instance.transform.parent = transform;
            newEnchantment = false;
		}
		
		
		//Play the sound effect.
		if(GameObject.Find("Voice") != null)
		{
			AudioSource audio_source = GameObject.Find("Voice").audio;
			
			if(audio_clip != null)
			{
				audio_source.audio.clip = audio_clip;
				audio_source.audio.Play();
			}
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
