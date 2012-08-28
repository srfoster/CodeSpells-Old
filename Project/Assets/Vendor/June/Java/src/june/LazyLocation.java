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
        String[] split = (Enchanted.executeCommand(this.toString())).split(",");
        String x_string = split[0].substring(1);
        String y_string = split[1];
        String z_string = split[2].substring(0, split[2].length() - 1);
        evals_to_vector3 = "(new Vector3("+x_string+","+y_string+","+z_string+"))";
        post_x = "";
        post_y = "";
        post_z = "";
    }
    
    @Override
    public double distanceBetween(Location loc) {
        return Double.parseDouble(Enchanted.executeCommand("Vector3.Distance("+this.toString()+","+loc.toString()+")"));
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
    public Vector3 times(double scale)
    {
      LazyLocation ret = new LazyLocation(evals_to_vector3);
      ret.appendPostX("*" + scale);
      ret.appendPostY("*" + scale);
      ret.appendPostZ("*" + scale);

      return ret;
    }

    @Override
    public Vector3 add(Vector3 loc)
    {
      LazyLocation ret = new LazyLocation(toString());
      ret.appendPostX("+" + loc.getXString());
      ret.appendPostY("+" + loc.getYString());
      ret.appendPostZ("+" + loc.getZString());

      return ret;
    }

    
    public String toString() {
        return "new Vector3("+getXString()+","+getYString()+","+getZString()+")";
    }


    public void setPostX(String string){
      post_x = string;
    }
    public void setPostY(String string){
      post_y = string;
    }
    public void setPostZ(String string){
      post_z = string;
    }
    public void appendPostX(String string){
      post_x += string;
    }
    public void appendPostY(String string){
      post_y += string;
    }
    public void appendPostZ(String string){
      post_z += string;
    }
}
