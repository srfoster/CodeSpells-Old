package june;

public class Movement
{
	Enchanted parent;

	/**
	 * Constructs a new instance.
	 *
	 * @param parent The parent for this instance.
	 */
    
    //location values
    
	public Movement(Enchanted parent)
	{
		this.parent = parent;
	}
    
    //new Location(0,0,0)
    public void teleport(Location loc) {
        parent.command("transform.position = new Vector3("+loc.getX()+","+loc.getY()+","+loc.getZ()+")");
    }
    
    
    //(x,z)
    //loc is the location that the object will be adjacent to
    
    
    

    
	public void forward(double distance)
	{
        double speed = 0.05;
        double oldPos = currentPosition();
        double newPos = oldPos+distance;
		while(currentPosition() < newPos)
		{
			parent.command("transform.position.x += Time.deltaTime *" + distance);
            
		}
	}
    
    
	public void backward(double distance)
	{
        double speed = 0.05;
        double oldPos = currentPosition();
        double newPos = oldPos-distance;
		while(currentPosition() > newPos)
		{
			parent.command("transform.position.x -= Time.deltaTime * " + distance);
		}
	}

	public void right(double distance)
	{
		parent.command("transform.position += objects['Player'].transform.right * " + distance);
	}

	public void left(double distance)
	{
		parent.command("transform.position -= objects['Player'].transform.right * " + distance);
	}

	public void levitate(double height, double speed)
	{
		double original_height = currentHeight();

		parent.command("rigidbody.useGravity = false");
		parent.command("rigidbody.isKinematic = false");
		parent.command("rigidbody.AddForce(Vector3.up * "+speed+")");

		System.out.println("HERE");
		while(currentHeight() - original_height < height)
		{
			//Spin and wait
			try{
				Thread.sleep(100);
			}catch(Exception e){

			}
		}

		parent.command("rigidbody.AddForce(Vector3.up * -"+speed+")");
		parent.command("rigidbody.isKinematic = true");
	}

	public void levitate(double height)
	{
		levitate(height, 10);
	}
    

	public void drop()
	{
		parent.command("rigidbody.isKinematic = false");
		parent.command("rigidbody.useGravity = true");
	}
    
	public double currentHeight()
	{
		return Double.parseDouble(parent.command("transform.position.y"));
	}
    
    /*comment 
     out 
     later*/
	public double currentPosition()
	{
		return Double.parseDouble(parent.command("transform.position.x"));
	}
}
