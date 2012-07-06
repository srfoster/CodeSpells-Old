package june;

public class Movement
{
	Enchanted parent;

	public Movement(Enchanted parent)
	{
		this.parent = parent;
	}

	public void levitate(double height, double speed)
	{
    double so_far = 0.0;

    while(so_far < height)
    {
        Log.log("In while loop because " +so_far+ "<" +height);
        parent.command("transform.position.y += " + speed);
        so_far += speed;
    }

    Log.log("Finished levitating");
        
    }

	public void levitate(double height)
	{
		levitate(height, 10);
	}
    
    public void rotate(double angle) {
        parent.command("transform.rotation.y = "+angle);
    }

	public void drop()
	{
		parent.command("rigidbody.isKinematic = false");
		parent.command("rigidbody.useGravity = true");
	}
    
}
