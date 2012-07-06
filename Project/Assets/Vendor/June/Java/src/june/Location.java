package june;

public class Location
{
    public double x;
    public double y;
    public double z;

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

    public void adjust(int dir)
    {
      adjust(dir, 1.0);
    }
    
    public void adjust(int dir, double scale) {
        Location adjustment = Direction.toLocation(dir); 
        adjustment.times(scale);

        add(adjustment);
    }

    public void add(Location l)
    {
      x += l.x;
      y += l.y;
      z += l.z;
    }

    public void times(double scale)
    {
      x *= scale;
      y *= scale;
      z *= scale;
    }

}
