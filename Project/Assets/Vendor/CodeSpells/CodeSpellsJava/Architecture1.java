import june.*;
import java.util.*;
      
public class Architecture1 extends Spell
{
    public void cast()
    { 
        Enchanted end = getByName("Area 1");
        Enchanted start = getByName("Area 2");

        Enchanted rock_area = getByName("Area 3");
        
        for(int i = 0; i < 50; i++)
          rock_area.scale();
        
        EnchantedList list = rock_area.findWithin();

        Direction direction = Direction.between(start,end);

        Location last = start.getLocation();
        
        for(Enchanted e : list)
        {
            last.add(direction);
            e.setLocation(last);
            last = e.getLocation();
        }

    }
}
