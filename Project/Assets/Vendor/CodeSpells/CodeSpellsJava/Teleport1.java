import june.*;
import java.util.*;

public class Teleport1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    Location dest = target.getLocation();
			dest.setY(dest.getY() + 10);

    getByName("Player").movement()
      .teleport(dest);  
  }
}
