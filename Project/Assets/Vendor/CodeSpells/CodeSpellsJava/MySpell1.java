import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    Enchanted thing = spawn("redCrate", l);
    thing.move(Direction.north(), 500f);
  }
}
