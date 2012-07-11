import june.*;
import java.util.*;

public class Teleport2 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    Location dest = target.getLocation();
    dest.adjust(Direction.up(), 10);

    getByName("Me").setLocation(dest); 
  }
}
