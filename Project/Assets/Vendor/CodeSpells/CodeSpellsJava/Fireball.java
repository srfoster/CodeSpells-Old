import june.*;
import java.util.*;

public class Fireball extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            
    Enchanted flamingRock = getByName("FlamingRock");

    Location dest = target.getLocation();

    flamingRock.setLocation(dest); 
  }
}
