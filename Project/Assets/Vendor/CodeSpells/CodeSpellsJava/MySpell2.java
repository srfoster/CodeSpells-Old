import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    Enchanted s = getTarget();
    s.move(Direction.North(), 300);
  }
}
