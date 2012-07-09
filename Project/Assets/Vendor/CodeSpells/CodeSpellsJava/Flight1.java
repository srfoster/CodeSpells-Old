import june.*;
import java.util.*;

public class Flight1 extends Spell
{
  public void cast()
  { 
    Enchanted my_target = getTarget();            

    AdeptLevitate1 lev = new AdeptLevitate1();

    while(true){
       my_target.move(Direction.FORWARD, 0.2);
    }  
  }
}

