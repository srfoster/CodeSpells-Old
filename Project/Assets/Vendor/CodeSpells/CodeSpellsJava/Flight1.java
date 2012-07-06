import june.*;
import java.util.*;

public class Flight1 extends Spell
{
  public void cast()
  { 
    Enchanted target = getTarget();            

    target.movement().levitate(10, 0.2);

    while(true){
       target.move(Direction.FORWARD, 0.2);
    }  
  }
}

