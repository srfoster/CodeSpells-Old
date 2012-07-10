import june.*;
import java.util.*;

public class MassiveFire extends Spell
{
  public void cast()
  { 
    Enchanted me   = getByName("Me");
    Enchanted rock = getByName("Rock");            

    EnchantedList list = me.findLike(rock, 100);
    list.setName("Rocks");

    for(Enchanted another_rock : list)
    {
      another_rock.onFire(true); 
    }
  }
}
