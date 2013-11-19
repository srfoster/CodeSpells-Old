private var motor : CharacterMotor;

var stop_up   = false;
var stop_down = false;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
}

// Update is called once per frame
function Update () {
	// Get the input vector from keyboard or analog stick
	var rotateLR : float = Input.GetAxis ("Horizontal"); 
	var directionVectorUD = new Vector3(0, 0, Input.GetAxis("Vertical") * 10 * Time.deltaTime);

	transform.Rotate(Vector3.up, rotateLR * 50 * Time.deltaTime);
	
	// To look up, push W
	if(Input.GetKey(KeyCode.W))
    {
		if(GameObject.Find("Main Camera").transform.rotation.x >= -0.25) {
   			GameObject.Find("Main Camera").transform.Rotate(Vector3.left, 0.8 * 60 * Time.deltaTime);
   		}

    }

	// To look down, push S
    if(Input.GetKey(KeyCode.S))
    {
    	if(GameObject.Find("Main Camera").transform.rotation.x <= 0.25) {
    		GameObject.Find("Main Camera").transform.Rotate(Vector3.left, -0.8 * 60 * Time.deltaTime);
    	}
    }
   
    // Code for strafing left and right. Could be implemented, but need to solve "infinite falling" problem
    // if you run into unlevel terrain
    
    /*
    if(Input.GetKey(KeyCode.LeftArrow))
    {
    		GameObject.Find("Main Camera").transform.Translate(-0.1 * 60 * Time.deltaTime, 0, 0);
    		transform.Translate(-0.1 * 60 * Time.deltaTime, 0, 0);
    }
    
    if(Input.GetKey(KeyCode.RightArrow))
    {
    		GameObject.Find("Main Camera").transform.Translate(0.1 * 60 * Time.deltaTime, 0, 0);
    		transform.Translate(0.1 * 60 * Time.deltaTime, 0, 0);
    }*/

     /*
    
	var rotationX = GameObject.Find("Main Camera").transform.localEulerAngles.x;
	var rotationY = GameObject.Find("Main Camera").transform.localEulerAngles.y;
	var rotationZ = GameObject.Find("Main Camera").transform.localEulerAngles.z;



	if (Input.GetKey(KeyCode.Q) && ((rotationX >= 0 && rotationX < 90) || (rotationX <= 360 && rotationX >= 270)))
	{
		rotationX += 120*Time.deltaTime;			
	}
	
	if(Input.GetKey(KeyCode.E) && ((rotationX >= 0 && rotationX <= 90) || (rotationX <= 360 && rotationX > 270)))
	{
		rotationX += -120*Time.deltaTime;
	}
	
	GameObject.Find("Main Camera").transform.localEulerAngles = new Vector3(rotationX, rotationY, rotationZ);

    */
	
	if (directionVectorUD != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLengthUD = directionVectorUD.magnitude;
		directionVectorUD = directionVectorUD / directionLengthUD;

		// Make sure the length is no bigger than 1
		directionLengthUD = Mathf.Min(1, directionLengthUD);

		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLengthUD = directionLengthUD * directionLengthUD;

		// Multiply the normalized direction vector by the modified length
		directionVectorUD = directionVectorUD * directionLengthUD;
	}

	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVectorUD;
	motor.inputJump = Input.GetButton("Jump");
	
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")