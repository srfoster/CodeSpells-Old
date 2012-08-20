import june.*;

public class LargeFire extends Spell{
  public void cast(){
    Enchanted flag = getTarget();
    flag.grow(50);
    EnchantedList eList = flag.findWithin();
    for (Enchanted e : eList) {
      if (e.isRock()) {
        e.onFire(true);
        }
      }
  }
}
