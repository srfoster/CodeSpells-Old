import june.*;
import java.util.*;
      
public class Architecture1 extends Spell
{
    public void cast()
    { 
        Enchanted end = getByName("End");
        Enchanted start = getByName("Start");
        
        EnchantedList list = getListByName("Rocks");
        Direction direction = Direction.between(start,end);

        Location last = start.getLocation();
        

         s      
        for(Enchanted e : list)
        {
            last.add(direction);
            e.setLocation(last);
            last = e.getLocation();
        }

    }
}
