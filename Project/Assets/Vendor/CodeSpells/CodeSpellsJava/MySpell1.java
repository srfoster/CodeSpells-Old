import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted e = getTarget();
    e.onFire(true);
    e.move(Direction.west(), 10);
    e.move(Direction.south(), 20);
  }
}
