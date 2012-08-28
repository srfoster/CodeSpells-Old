import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    Enchanted s = spawn("redCrateNG", l);
    s.move(Direction.up(), 20);
  }
}
