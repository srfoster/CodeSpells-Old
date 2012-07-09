package june;

public class Location implements Vector3
{
    public double x;
    public double y;
    public double z;

    public Location(){

    }

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

    public void adjust(Vector3 dir)
    {
      adjust(dir, 1.0);
    }
    
    public void adjust(Vector3 dir, double scale) {
        dir.times(scale);

        add(dir);
    }

    public void add(Vector3 l)
    {
      x += l.getX();
      y += l.getY();
      z += l.getZ();
    }

    public void times(double scale)
    {
      x *= scale;
      y *= scale;
      z *= scale;
    }

    public String getXString(){
      return "" + x;
    }

    public String getYString(){
      return "" + y;
    }

    public String getZString(){
      return "" + z;
    }
}
