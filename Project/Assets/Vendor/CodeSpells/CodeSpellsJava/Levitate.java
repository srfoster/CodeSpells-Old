
import june.*;
import java.util.*;

public class Levitate extends Spell
{
  public void cast()
  { 
    Enchanted target = getByName();            

    Location dest = target.getLocation();
			dest.setY(dest.getY() + 10);

    getByName("Player").movement().teleport(dest);   
  }
}












