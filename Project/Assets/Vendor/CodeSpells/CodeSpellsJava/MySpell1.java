import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    Enchanted s = spawn("redCrate", l);
    s.grow(2);
  }
}
