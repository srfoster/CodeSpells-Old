using UnityEngine;
using System.Collections;

public class FlyThrough : MonoBehaviour {
	
	public delegate void EventHandler(float percent);
	public static event EventHandler FlyThroughPercentFinished;

	bool record = false;
	
	string[] frames;
		
	int current = 0;
	
	bool done = false;
	

	
	void Start(){
		if(!record){
			frames = System.IO.File.ReadAllLines(Application.dataPath + "/Vendor/Level1/FlyThrough/camera_script.txt");
			
			Camera.main.GetComponent<MouseLook>().enabled = false;
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(done)
			return;		
		
		if(FlyThroughPercentFinished != null)
			FlyThroughPercentFinished((current * 1f + 1) / (frames.Length * 1.0f));

		if(record)
		{
			transform.position += Camera.main.transform.forward * Input.GetAxis("Vertical") *0.2f
				                  + Camera.main.transform.right * Input.GetAxis("Horizontal") *0.2f;
			
			
			System.IO.File.AppendAllText("/Users/stephenfoster/Desktop/camera_script.txt", transform.position.x + "," + transform.position.y + "," + transform.position.z + "," + Camera.main.transform.rotation.x + "," + Camera.main.transform.rotation.y + "," + Camera.main.transform.rotation.z + "," + Camera.main.transform.rotation.w + "\n");
		} else {
			if(current < frames.Length)
			{

				
				string[] info = frames[current++].Split(',');
				
				float loc_x = float.Parse(info[0]);
				float loc_y = float.Parse(info[1]);
				float loc_z = float.Parse(info[2]);
				float rot_x = float.Parse(info[3]);
				float rot_y = float.Parse(info[4]);
				float rot_z = float.Parse(info[5]);
				float rot_w = float.Parse(info[6]);
				
				transform.position = new Vector3(loc_x, loc_y, loc_z);
				
				Camera.mainCamera.transform.rotation = new Quaternion(rot_x, rot_y, rot_z, rot_w);
			} else {
				done = true;
			}
		}
	}
}
