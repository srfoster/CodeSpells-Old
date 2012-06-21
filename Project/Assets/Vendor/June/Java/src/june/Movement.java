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
			parent.command("transform.position.x += Time.deltaTime*" + speed);
		}
	}

	public void backward(double distance)
	{
        double speed = 0.05;
        double oldPos = currentPosition();
        double newPos = oldPos+distance;
		while(currentPosition() > newPos)
		{
			parent.command("transform.position.x -= Time.deltaTime * " + speed);
		}
	}

	public void right(double distance)
	{
		parent.command("transform.position.z += Time.deltaTime * "+distance+"\n");
	}

	public void left(double distance)
	{
		parent.command("transform.position.z -= Time.deltaTime * "+distance+"\n");
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
    
    public void transport(double distance, double height, String direction)
    {
        parent.command("rigidbody.useGravity = false");
		double liftSpeed = 0.03;
        double moveSpeed = 0.05;
		while(currentHeight() < height)
		{
            parent.command("transform.position.y += Time.deltaTime" + liftSpeed);
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
		parent.command("rigidbody.isKinematic = false");
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
