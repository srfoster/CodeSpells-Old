import june.*;
import java.util.*;

public class Levitate1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    target.movement().levitate(3.0f, .1f);
  }
}
