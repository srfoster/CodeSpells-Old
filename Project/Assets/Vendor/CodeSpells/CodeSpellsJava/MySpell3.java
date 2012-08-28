import june.*;

public class MySpell3 extends Spell{
  public void cast(){
<<<<<<< HEAD
    Enchanted crate = getTarget();
    Location cLoc = crate.getLocation();
    Direction up = Direction.up();
    cLoc.add(up);
    spawn("redCrate", cLoc);
=======
    spawn("redCrate", getTarget().getLocation());
>>>>>>> de4109edcf624e635babfbfe75d37bd8a54108d1
  }
}
