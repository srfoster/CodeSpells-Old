package june;

public class Movement
{
	Enchanted parent;

	/**
	 * Constructs a new instance.
	 *
	 * @param parent The parent for this instance.
	 */
	public Movement(Enchanted parent)
	{
		this.parent = parent;
	}

	public void forward(double distance)
	{
        double speed = 0.05;
        double oldPos = currentPosition();
        double newPos = oldPos+distance;
		while(currentPosition() < newPos)
		{
			parent.command("transform.position.x += " + speed);
		}
	}

	public void backward(double distance)
	{
        double speed = 0.05;
        double oldPos = currentPosition();
        double newPos = oldPos+distance;
		while(currentPosition() > newPos)
		{
			parent.command("transform.position.x -= " + speed);
		}
	}

	public void right(double distance)
	{
		parent.command("transform.position.z += "+distance+"\n");
	}

	public void left(double distance)
	{
		parent.command("transform.position.z -= "+distance+"\n");
	}

	public void levitate(double height)
	{
		parent.command("rigidbody.useGravity = false");
		double speed = 0.03;
		while(currentHeight() < height)
		{
			parent.command("transform.position.y += " + speed);
		}
	}
    
    public void transport(double distance, double height, String direction)
    {
        parent.command("rigidbody.useGravity = false");
		double liftSpeed = 0.03;
        double moveSpeed = 0.05;
		while(currentHeight() < height)
		{
            parent.command("transform.position.y += " + liftSpeed);
		}
        double curr = currentPosition();
        
        if(direction.equals("Forward"))
        {
            forward(distance);
        }  
        else if(direction.equals("Backward"))
        {
            backward(distance);
        }
    }
	
	public void drop()
	{
		parent.command("rigidbody.useGravity = true");
	}

	public double currentHeight()
	{
		return Double.parseDouble(parent.command("transform.position.y"));
	}
	public double currentPosition()
	{
		return Double.parseDouble(parent.command("transform.position.x"));
	}
}
