import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    for(int i=0; i < 10; i++)
    {
      Location l = mod(0,i*.5,i*3, getTarget().getLocation());
 
      spawn("redCrateNG", l);
    }
  }
 
  public Location mod(double x, double y, double z, Location loc)
  {
    Location ret = loc;
 
    ret = (Location) ret.add(Direction.forward().times(z));
    ret = (Location) ret.add(Direction.right().times(x));
    ret = (Location) ret.add(Direction.up().times(y));
 
    return ret;
   }
}
