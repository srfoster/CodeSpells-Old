package june;

public class LazyDirection extends Direction
{
    String evals_to_vector3;
    String post_x = "";
    String post_y = "";
    String post_z = "";

    public LazyDirection(String string)
    {
      evals_to_vector3 = "("+string+")"; 
    }

    public LazyDirection(double x, double y, double z)
    {
      evals_to_vector3 = "(new Vector3("+x+","+y+","+z+"))";
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
      LazyDirection ret = new LazyDirection(evals_to_vector3);
      ret.appendPostX("*" + scale);
      ret.appendPostY("*" + scale);
      ret.appendPostZ("*" + scale);

      return ret;
    }

    @Override
    public Vector3 add(Vector3 loc)
    {
      LazyDirection ret = new LazyDirection(evals_to_vector3);
      ret.appendPostX("+" + loc.getXString());
      ret.appendPostY("+" + loc.getYString());
      ret.appendPostZ("+" + loc.getZString());

      return ret;
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
