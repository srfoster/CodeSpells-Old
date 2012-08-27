import june.*;
import java.util.*;

public class Tower extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();
    
    Location orig = target.getLocation(); 
    
    Direction dir = Direction.up();

    Vector3 dest = orig.add(dir.times(10));

    Enchanted spawned = spawn("redCrate", (Location) dest);         
  }
}
