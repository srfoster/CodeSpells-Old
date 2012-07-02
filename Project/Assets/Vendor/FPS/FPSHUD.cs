using UnityEngine;
using System.Collections;

public class FPSHUD : MonoBehaviour 
{


// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.
 
public  float updateInterval = 0.5F;
 
private float accum   = 0; // FPS accumulated over the interval
private int   frames  = 0; // Frames drawn over the interval
private float timeleft; // Left time for current interval
	
private string format = "0";
private Color color = new Color();
	
void Start()
{
    timeleft = updateInterval;  
}
 
void OnGUI()
{
    timeleft -= Time.deltaTime;
    accum += Time.timeScale/Time.deltaTime;
    ++frames;
    
    // Interval ended - update GUI text and start new interval
    if( timeleft <= 0.0 )
    {
        // display two fractional digits (f2 format)
	    float fps = accum/frames;
	    format = System.String.Format("{0:F2} FPS",fps);
				
		if(fps < 10)
	        color = Color.red;		
	    else if(fps < 30)
	        color = Color.yellow;
	    else 
	        color = Color.green;
			
    	//  DebugConsole.Log(format,level);
        timeleft = updateInterval;
        accum = 0.0F;
        frames = 0;
    }
		
	display(format,color);
}
	
	void display(string format, Color color)
	{
		GUIStyle style = new GUIStyle();
		style.normal.textColor = color;
		GUI.Label(new Rect(10,10,100,20), format, style);	
	}
}