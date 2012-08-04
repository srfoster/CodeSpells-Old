import june.*;
import java.util.*;

public class AdeptLevitate1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getByName("Server");
    int counter = 0;
    while(counter < 100)
    {
      my_target.move(Direction.up(), .1);
      counter = counter + 1;
    }
  }
}

