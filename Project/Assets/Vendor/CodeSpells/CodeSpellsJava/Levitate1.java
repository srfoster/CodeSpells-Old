import june.*;
import java.util.*;

public class Levitate1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();
    int counter = 0;
    while(counter < 120) {
    my_target.move(Direction.up(), 0.1);
      counter++;
      }

  }
}

