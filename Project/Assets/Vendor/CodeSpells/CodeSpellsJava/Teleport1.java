import june.*;
import java.util.*;

public class Teleport1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    Location dest = target.getLocation();
			dest.adjust(Direction.UP, 100);

    getByName("Me").setLocation(dest);  
  }
}
