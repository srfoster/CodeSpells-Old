import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        while(true)
        {
           Enchanted a1 = getByName("Area 1");

           EnchantedList within = a1.findWithin();

           if(within.get(0) != null)
           {

           }
        }
    }
}


