import june.*;
import java.util.*;

public class Levitate1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();

    my_target.move(Direction.UP, 0.1);
    my_target.move(Direction.UP, 0.1);
    my_target.move(Direction.UP, 0.1);
    my_target.move(Direction.UP, 0.1);
    my_target.move(Direction.UP, 0.1);
  }
}

