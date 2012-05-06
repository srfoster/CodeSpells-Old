var xSpeed : float = 1;
var ySpeed : float = 1;
var zSpeed : float = 1;

var manual : boolean = false;

function Update () 
{
	
	if( !manual )
	{
	
		transform.RotateAround( transform.position, Vector3.right, ySpeed * Time.deltaTime );
		transform.RotateAround( transform.position, Vector3.up, xSpeed * Time.deltaTime );
		transform.RotateAround( transform.position, Vector3.forward, zSpeed * Time.deltaTime );
		
	}
	else
	{
		
		if( Input.GetAxis("Horizontal") != 0 )
		{
			
			transform.RotateAround( transform.position, Vector3.up, Input.GetAxis("Horizontal")*xSpeed * Time.deltaTime );
			
		}
		
		if( Input.GetAxis("Vertical") != 0 )
		{
			
			transform.RotateAround( transform.position, Vector3.right, Input.GetAxis("Vertical")*ySpeed * Time.deltaTime );
			
		}
		
	}
	
}