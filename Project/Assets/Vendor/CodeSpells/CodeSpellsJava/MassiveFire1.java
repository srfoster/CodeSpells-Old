import june.*;
import java.util.*;

public class MassiveFire1 extends Spell
{
  public void cast()
  { 
    Enchanted me   = getByName("Me");
    Enchanted rock = getByName("Rock");            

    EnchantedList list = me.findLike(rock, 10);
    list.setName("Rocks");

    for(Enchanted another_rock : list)
    {
      another_rock.onFire(true); 
    }
  }
}
