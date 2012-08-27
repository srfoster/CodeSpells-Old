import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted target = getTarget();
    target.grow(10);
    EnchantedList eList = target.findWithin();
    for (Enchanted e : eList) {
      e.onFire(true);
      e.move(Direction.up(), 10);
      }
  }
}
