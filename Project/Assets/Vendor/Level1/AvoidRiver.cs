using UnityEngine;
using System.Collections;

public class AvoidRiver : MonoBehaviour {
	public float alphaFadeValue = 1;
	public bool is_dying = false;
	public float time_waited = 0;
	public Texture2D fade_texture;
	public AudioClip dying;
	public bool death_started = false;
	
	void OnTriggerEnter (Collider collider) {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		// Make sure that the player was the one that collided with the boundary
		if(collider.gameObject != player)
			return;
		
		KillPlayer();
	}
	
 	void KillPlayer() 
	{
    	is_dying = true;
  	}
  
  	void Update()
  	{
		if(is_dying)
    	{
			if(!death_started)
			{
				GameObject.Find("Voice").audio.PlayOneShot(dying);
				death_started = true;
			}
      		time_waited += Time.deltaTime;
      
      		if(time_waited >= 2)
     		{
				
				GameObject start = GameObject.FindGameObjectWithTag("Respawn");
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				
        		player.transform.position = start.transform.position;
				time_waited = 0;
				alphaFadeValue = 1;
				is_dying = false;
				death_started = false;
      		}
    	}
  	}
  
  	void OnGUI()
  	{
    	if(is_dying)
    	{
      		alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);

      		GUI.color = new Color(0, 0, 0, 1 - alphaFadeValue);
      		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), fade_texture); 
    	}
  	}
}
