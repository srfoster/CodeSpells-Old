import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted en = spawn("redCrateNG", getTarget().getLocation());
    en.move(Direction.up(), 7.0);
  }
}
