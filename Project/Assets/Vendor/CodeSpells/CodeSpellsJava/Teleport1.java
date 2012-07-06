import june.*;
import java.util.*;

public class Teleport1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    while(true){ target.move(Direction.RIGHT); }  
  }
}
