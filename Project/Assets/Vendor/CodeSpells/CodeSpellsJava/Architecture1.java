import june.*;
import java.util.*;
      
public class Architecture1 extends Spell
{
    public void cast()
    { 
        Enchanted start = getByName("Start");
        Enchanted destination = getTarget();
        Enchanted center = getByName("Me");
        
        EnchantedList list = center.findLike(start, 25.0);

        Direction direction = Direction.between(start,destination);
        int size = list.size();
        
        Location last = start.getLocation();
        //make iterable next
        for(int i = 0; i < size; i++)
        {
            last.add(direction);
            list.get(i).setLocation(last);
            last = list.get(i).getLocation();
        }
    }
}
