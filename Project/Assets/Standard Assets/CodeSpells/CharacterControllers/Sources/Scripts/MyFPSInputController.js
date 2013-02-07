private var motor : CharacterMotor;

var stop_up   = false;
var stop_down = false;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
}

// Update is called once per frame
function Update () {
	// Get the input vector from kayboard or analog stick
	var directionVector = new Vector3(0, 0, Input.GetAxis("Vertical"));
	transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * 120 * Time.deltaTime);

	// To look up, push R
	if(Input.GetKey(KeyCode.R))
    {
   		GameObject.Find("Main Camera").transform.Rotate(Vector3.left, 1 * 120 * Time.deltaTime);
    }

	// To look down, push F
    if(Input.GetKey(KeyCode.F))
    {
    	GameObject.Find("Main Camera").transform.Rotate(Vector3.left, -1 * 120 * Time.deltaTime);
    }

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

	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;

		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);

		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;

		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}

	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	motor.inputJump = Input.GetButton("Jump");
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")