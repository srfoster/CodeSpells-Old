import june.*;
 
public class TrickyShoot extends Spell{
  public void cast(){
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
  }
}
