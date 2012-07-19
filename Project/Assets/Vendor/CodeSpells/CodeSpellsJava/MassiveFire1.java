import june.*;
import java.util.*;

public class MassiveFire1 extends Spell
{
  public void cast()
  { 
    Enchanted area   = getByName("Area 1");



    EnchantedList list = area.findWithin();

    for(Enchanted e : list)
      e.onFire(true);
  }
}
