using UnityEngine;
using System.Collections;

public class EndFlyThrough : MonoBehaviour {
	float alphaFadeValue = 0;
	bool done = false;

	// Use this for initialization
	void Start () {
		FlyThrough.FlyThroughPercentFinished += (percent) => {
			
			//Debug.Log(percent);
			
			if(percent > .96 && !done)
			{
				alphaFadeValue = 1;
				done = true;
			}
		};
	}
  
  	void OnGUI()
  	{
    	if(alphaFadeValue > 0 || done)
    	{
      		alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);

      		GUI.color = new Color(0, 0, 0, 1 - alphaFadeValue);
      		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), Resources.Load("monster") as Texture2D); 
    	}
  	}
	

}
