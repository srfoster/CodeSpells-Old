import june.*;
import java.util.*;

public class AdeptLevitate1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();
    int counter = 0;
    while(counter < 100)
    {
      my_target.move(Direction.up(), 0.1);
      counter = counter + 1;
    }
  }
}

