package june;

public class LazyLocation extends Location
{
    String evals_to_vector3; //normalized direction
    String post_x = "";
    String post_y = "";
    String post_z = "";

    public LazyLocation(String string)
    {
      evals_to_vector3 = "("+string+")";
    }

    public LazyLocation(double x, double y, double z) {
      evals_to_vector3 = "(new Vector3("+x+","+y+","+z+"))";
    }
    
    @Override
    public void freeze() 
    {
        String[] split = (Enchanted.commandGlobal(this.toString())).split(",");
        String x_string = split[0].substring(1);
        String y_string = split[1];
        String z_string = split[2].substring(0, split[2].length() - 1);
        evals_to_vector3 = "(new Vector3("+x_string+","+y_string+","+z_string+"))";
    }
    
    @Override
    public double distanceBetween(Location loc) {
        return Double.parseDouble(Enchanted.commandGlobal("Vector3.Distance("+this.toString()+","+loc.toString()+")"));
    }

    @Override
    public String getXString()
    {
      return evals_to_vector3 +".x" + post_x;
    }

    @Override
    public String getYString()
    {
      return evals_to_vector3 +".y" + post_y;
    }

    @Override
    public String getZString()
    {
      return evals_to_vector3 +".z" + post_z;
    }


    @Override
    public void times(double scale)
    {
      post_x += "*" + scale;      
      post_y += "*" + scale;      
      post_z += "*" + scale;      
    }

    @Override
    public void add(Vector3 loc)
    {
      post_x += "+" + loc.getXString();      
      post_y += "+" + loc.getYString();      
      post_z += "+" + loc.getZString();      
    }
    
    public String toString() {
        return "new Vector3("+getXString()+","+getYString()+","+getZString()+")";
    }
}
