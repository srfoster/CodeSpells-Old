package june;

public class Location
{
    private double x;
    private double y;
    private double z;

    public Location(String xyz)
    {
	String[] split = xyz.split(",");

	String x_string = split[0].substring(1);
	String y_string = split[1];
	String z_string = split[2].substring(0, split[2].length() - 1);

	x = Double.parseDouble(x_string);
	y = Double.parseDouble(y_string);
	z = Double.parseDouble(z_string);
    }
    
    public Location(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public double getX() {
        return x;
    }
    
    public double getY() {
        return y;
    }
    
    public double getZ() {
        return z;
    }

    public void setX(double x){
      this.x = x;
    }

    public void setY(double y){
      this.y = y;
    }

    public void setZ(double z){
      this.z = z;
    }

    public String toString()
    {
	return "(" + x + " " + y + " " + z + ")";
    }
    
}
