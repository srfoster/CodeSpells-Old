import june.*;
import java.util.*;

public class Levitate1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();
    int i = 0;
    
    while(true)
    {
      while(i < 10)
      {
        my_target.move(Direction.up(), 0.1);
        i++;
      }
 
      while(i > 0)
      {
        my_target.move(Direction.down(), 0.1);
        i--;
      }
    }
  }
}

