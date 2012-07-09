package june;

public class Direction implements Vector3
{
    public double x;
    public double y;
    public double z;

    public Direction(){

    }

    public Direction(String xyz)
    {
        String[] split = xyz.split(",");

        String x_string = split[0].substring(1);
        String y_string = split[1];
        String z_string = split[2].substring(0, split[2].length() - 1);

        x = Double.parseDouble(x_string);
        y = Double.parseDouble(y_string);
        z = Double.parseDouble(z_string);
    }
    
    public Direction(double x, double y, double z) {
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


    public static Direction up(){
        return new Direction(0,1,0);
    }

    public static Direction down(){
        return new Direction(0,-1,0);
    }

    public static Direction west(){
        return new Direction(-1,0,0);
    }
    
    public static Direction east(){
        return new Direction(1,0,0);
    }
    
    public static Direction north(){
        return new Direction(0,0,1);
    }
    
    public static Direction south(){
        return new Direction(0,0,-1);
    }
    
    public static Direction right() {
        String vector3_string = "objects['Me'].transform.right";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }
    
    public static Direction left() {
        String vector3_string = "objects['Me'].transform.right * -1";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction forward() {
        String vector3_string = "objects['Me'].transform.forward";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction backward() {
        String vector3_string = "objects['Me'].transform.forward * -1";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction none() {
        return new Direction(0,0,0);
    }

    public static Direction between(Enchanted source, Enchanted target)
    {
       String vector3_string = "(objects['"+target.getId()+"'].transform.position - objects['"+source.getId()+"'].transform.position).normalized";
            
       Direction dir = new LazyDirection(vector3_string);

       return dir;
    }
}
