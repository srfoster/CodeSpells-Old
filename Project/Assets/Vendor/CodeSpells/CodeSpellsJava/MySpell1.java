import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted c1 = makeBox();
 
     while(true)
     {
 
      boxInFront(c1);
 
 
      if(isJumping())
        //popup("Jumping!");
        spawnBoxes(c1);
      
 
    }
  }
 
 
  public boolean isJumping()
  {
      LazyLocation l = (LazyLocation) getByName("Me").getLocation();
      l.freeze();
 
 
      String s = l.getYString();
 
      String[] split_s = s.split(",");
      String y_string = split_s[1];
      double y = Double.parseDouble(y_string);
 
      return y > 3.0;
 
  }
 
  public void popup(String s)
  {
      (new Enchanted("")).executeCommand("util.popup('"+s+"')");
  }
 
  public void boxInFront(Enchanted c1)
  {
 
    Direction dir = Direction.forward();
    Vector3 dest = getByName("Me").getLocation();
    dest = dest.add(dir.times(30));
    c1.setLocation((Location)dest);
 
  }
 
  public Enchanted makeBox()
  {
 
    Enchanted e = getByName("Area 1");
    Enchanted me = getByName("Me");
    Enchanted c1 = spawn("redCrateNG", e.getLocation());
 
    return c1;
  }
  
  public void spawnBoxes(Enchanted c1)
  {
    EnchantedList list = new EnchantedList();
 
    for(int i=0; i < 5; i++)
    {
      for(int j = 0; j < 5; j++)
      {
        Location l = mod(i*2.5,0,j*2.5, c1.getLocation());
 
        Enchanted e = spawn("redCrateNG", l);
 
        list.add(e);
      }
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
