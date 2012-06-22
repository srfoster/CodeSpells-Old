using UnityEngine;
using System.Collections;

public class SwampEnterEffect : MonoBehaviour {
	private bool inside = false;
	
	public Color target_color = new Color(47.0f/255,93.0f/255,6.0f/255, 1);
	public Color target_fog_color = new Color(178.0f/255,215.0f/255,115.0f/255, 1);
	public float target_fog_density = 0.1f;
	
	private Color original_color;
	private Color original_fog_color;
	private float original_fog_density;
	private Material original_skybox;
	
	public float speed = 3;
	
	private float time_inside;
	
	private bool pause = true;
	
	void Start()
	{
		//Hack because changes to the skybox material are, for some reason, permanent.  (They persist across runs of the game.)
		(GameObject.Find("Main Camera").GetComponent("Skybox") as Skybox).material.shader =Shader.Find( "RenderFX/Skybox" );

	}
	
	void Update()
	{
		if(pause)
			return;
		
		time_inside += Time.deltaTime;
		float percent = Mathf.Min(time_inside/speed, 1);
		
		
		if(inside)
		{	
			//RenderSettings.ambientLight = fadeFrom(original_color,target_color, percent);
			RenderSettings.fogDensity = fadeFrom(original_fog_density, target_fog_density, percent);
			RenderSettings.fogColor = fadeFrom(original_fog_color, target_fog_color, 1);
			
			
			(GameObject.Find("Main Camera").GetComponent("Skybox") as Skybox).material.shader =Shader.Find( "Diffuse" );
		
			GameObject.Find("SwampSounds").audio.volume = fadeFrom(0,1, percent);
		} else {
			//RenderSettings.ambientLight = fadeFrom(target_color,original_color, percent);
			RenderSettings.fogDensity = fadeFrom(target_fog_density, original_fog_density, percent);
			RenderSettings.fogColor = fadeFrom(target_fog_color, original_fog_color, percent);
			
			(GameObject.Find("Main Camera").GetComponent("Skybox") as Skybox).material.shader =Shader.Find( "RenderFX/Skybox" );
			
			GameObject.Find("SwampSounds").audio.volume = fadeFrom(1,0, percent);

			
			//RenderSettings.fogColor = fadeFrom(target_fog_color, original_fog_color, 1);
		}
	}
	
	Color fadeFrom(Color f, Color s, float percent)
	{
		float red = fadeFrom(f.r,s.r,percent);
		float green = fadeFrom(f.g,s.g,percent);
		float blue = fadeFrom(f.b,s.b,percent);
		float alpha = fadeFrom(f.a,s.a,percent);

		return new Color(red,green,blue,alpha); 
	}
	
	float fadeFrom(float f, float s, float percent)
	{
		return f + (s - f) * percent;
	}
	
	
	void OnTriggerEnter(Collider col)
	{
		
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		//Only need to do this once.
	//	if(original_color == null)
	//	{
			original_color = RenderSettings.ambientLight;
			original_fog_color = RenderSettings.fogColor;
			original_fog_density = RenderSettings.fogDensity;
			original_skybox = RenderSettings.skybox;
	//	}
		inside = true;
		
		time_inside = 0;
		
		pause = false;
	}
	
	
	void OnTriggerExit(Collider col)
	{
		
		if(!col.gameObject.tag.Equals("Player"))
			return;
		
		inside = false;
		time_inside = 0;
		pause = false;
	}
}
