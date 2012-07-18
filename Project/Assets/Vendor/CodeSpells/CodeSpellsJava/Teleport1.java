import june.*;
import java.util.*;

public class Teleport1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            
    Enchanted myself = getByName("Me");

    Location dest = target.getLocation();
    dest.adjust(Direction.up(), 10);

    myself.setLocation(dest); 
  }
}
