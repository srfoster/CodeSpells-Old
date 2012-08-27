import june.*;
import java.util.*;

public class Tower extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            
    Enchanted myself = getByName("Me");

    Location temp = target.getLocation();
    Location dest = temp.adjust(Direction.up(), 10);

    myself.setLocation(dest); 
  }
}
