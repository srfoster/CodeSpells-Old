import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        Enchanted me = getByName("Player");
        Enchanted rock = getTarget();
              
        EnchantedList list = me.findLike(rock, 11.0); //try to get 10 rocks

        int size = (list.size() < 11) ? list.size() : 10;
        Enchanted current = list.get(0);
        double initY = current.getLocation().getY();
              
        for(int i = 0; i < size; i++)
        {
       	  Location dest = current.getLocation(Direction.EAST);			
          current = list.get(i);
          dest.setY(initY + 1f/5*(-i*i + size*i));
          current.setLocation(dest);
        }
              
        Location bridgeLoc = rock.getLocation(Direction.WEST);
        list.setLocation(bridgeLoc);
    }
}
