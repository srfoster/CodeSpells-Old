using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {
	
	bool audio_on = false;
	
	Vector3 last_player_location;

	// Use this for initialization
	void Start () {
		play_audio(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(player_is_moving() && !audio_on)
		{
			play_audio(true);	
		}
		
		if(!player_is_moving() && audio_on)
		{
			play_audio(false);
		}
	}
	
	private bool player_is_moving()
	{
		return Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0;
	}
	
	private void play_audio(bool on)
	{
		audio_on = on;
		
		if(on)
		{
		   GameObject.Find("FootSteps").audio.enabled = true;
		} else {
		   GameObject.Find("FootSteps").audio.enabled = false;	
		}
	}
}
