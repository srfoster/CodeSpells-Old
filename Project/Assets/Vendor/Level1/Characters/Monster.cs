using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	
	//Because the actual monster asset is a child of the monster prefab
	public GameObject child;
	public int speed = 10;
	public bool attacking = false;
	
	float alphaFadeValue = 0;
	float amount_rotated = 0f;

	bool alive = true;
	bool turning = true;
	
	Vector3 starting_location;
	Quaternion starting_rotation;
	
	void Start(){
		starting_location = transform.position;
		starting_rotation = transform.rotation;
	}
	
	public void Attack () {
		StartRunning();
		attacking = true;
	}
	
	void Update()
	{
	   	if(attacking)
	   	{
			UpdateAttack();
		} else {
			UpdateIdle();	
		}
	}
	
	void UpdateIdle(){
						
		Vector3 destination = GameObject.FindWithTag("Player").transform.position;
		
		Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.time*0.01f);
		
		bool x_eq = Mathf.Abs(Quaternion.Inverse(rotation).x) - Mathf.Abs(transform.rotation.x) <= 0.001;
		bool y_eq = Mathf.Abs(Quaternion.Inverse(rotation).y) - Mathf.Abs(transform.rotation.y) <= 0.001;
		bool z_eq = Mathf.Abs(Quaternion.Inverse(rotation).z) - Mathf.Abs(transform.rotation.z) <= 0.001;
		bool w_eq = Mathf.Abs(Quaternion.Inverse(rotation).z) - Mathf.Abs(transform.rotation.z) <= 0.001;

		if(x_eq && y_eq && z_eq && w_eq)
		{
			child.animation.Stop();	
		} else {
			child.animation.CrossFade("walk");	
		}
	}
	
	void UpdateAttack()
	{
		Vector3 destination = GameObject.FindWithTag("Player").transform.position;
			
		transform.LookAt(destination);
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
			
		transform.position = new Vector3(transform.position.x,
				Terrain.activeTerrain.SampleHeight(transform.position),
				transform.position.z);
			
		if(Vector3.Distance(destination, transform.position) <= 5 && alive)
		{
			StartCoroutine(KillPlayer());
			alive = false;
		}	
	}
	
	public void StartRunning()
	{
		child.animation["run"].speed = 2;
		
		child.animation.CrossFade("run");
	}
	
 	IEnumerator KillPlayer() 
	{
		alphaFadeValue = 1;
		
		GameObject.Find("Voice").audio.PlayOneShot(Resources.Load("Dying") as AudioClip);  

		
		yield return new WaitForSeconds(2);
		
		GameObject start = GameObject.FindGameObjectWithTag("Respawn");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		player.transform.position = start.transform.position;
		
		//reset everything;
		alive = true;
		attacking = false;
		alphaFadeValue = 0;
		
		transform.position = starting_location;
		
		transform.rotation = starting_rotation;
		
		child.animation.Stop();
  	}
  
  	void OnGUI()
  	{
    	if(alphaFadeValue > 0)
    	{
      		alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);

      		GUI.color = new Color(0, 0, 0, 1 - alphaFadeValue);
      		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), Resources.Load("monster") as Texture2D); 
    	}
  	}
	
	
}
