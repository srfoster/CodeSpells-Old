import june.*;

public class Fire extends Spell{
  public void cast(){
    Enchanted ench = getTarget();
    ench.grow(10);
    EnchantedList eList = ench.findWithin();
    
    for (Enchanted e: eList) {
      e.onFire(true);
      }
  }
}
