import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    Enchanted s = spawn("redCrateNG", l);
    s.move(Direction.up(), 25);
  }
}
