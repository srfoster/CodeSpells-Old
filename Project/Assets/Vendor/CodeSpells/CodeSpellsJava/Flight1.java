import june.*;
import java.util.*;

public class Flight1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();
    Enchanted rock = getByName("Target");            

    int counter = 0;
    while(counter < 30)
    {
      my_target.move(Direction.up(), 0.1);
      counter = counter + 1;
    }

    while(true){
       my_target.move(Direction.between(my_target, rock), 0.2);
    }  
  }
}

