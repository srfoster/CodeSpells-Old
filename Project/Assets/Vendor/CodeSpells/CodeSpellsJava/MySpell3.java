import june.*;
 
public class TrickyShoot extends Spell{
  public void cast(){
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    Enchanted crate = getTarget();
    Location cLoc = crate.getLocation();
    Direction up = Direction.up();
    cLoc.add(up);
    spawn("redCrate", cLoc);
=======
    spawn("redCrate", getTarget().getLocation());
>>>>>>> de4109edcf624e635babfbfe75d37bd8a54108d1
=======
    Enchanted e = getByName("Area 1");
=======
>>>>>>> 36fb2109e75ee725ae5ca250c4235c32b501a9b6
    Enchanted me = getByName("Me");
 
    Enchanted c1 = spawn("redCrate", me.getLocation());
 
    double i = 0;
    while(true){
      double x = Math.sin(i)*20;
 
      Direction dir = Direction.forward();
 
      Vector3 dest = me.getLocation();
      dest = dest.add(dir.times(25+x));
 
      c1.setLocation((Location)dest);
 
      i++;
    }
>>>>>>> 422fb8412fe4776f4955a0e7c59cf5481a933be5
  }
}
