import june.*;

public class SetFire extends Spell{
  public void cast(){
    Enchanted tar = getTarget();
    tar.grow(10);
    EnchantedList eList = tar.findWithin();
    for (Enchanted e : eList) {
      if (e.isRock()) {
        e.onFire(true);
        }
      }
  }
}
