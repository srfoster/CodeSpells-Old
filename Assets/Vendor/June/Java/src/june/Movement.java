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
		parent.command("transform.position.x += "+distance+"\n");
	}

	public void backward(double distance)
	{
		parent.command("transform.position.x -= "+distance+"\n");
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
	
	public void drop()
	{
		parent.command("rigidbody.useGravity = true");
	}

	public double currentHeight()
	{
		return Double.parseDouble(parent.command("transform.position.y"));
	}
}
