import june.*;

public class MySpell3 extends Spell{
  public void cast(){
    Enchanted crate = getTarget();
    Location cLoc = crate.getLocation();
    Direction up = Direction.up();
    cLoc.add(up);
    spawn("redCrate", cLoc);
  }
}
