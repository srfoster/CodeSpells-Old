import june.*;
import java.util.*;
      
public class Arch1 extends Spell
{
    public void cast()
    { 
        Enchanted me = getByName("Player");
        Enchanted rock = getTarget();
              
        EnchantedList list = me.findLike(rock, 11.0); //try to get 10 rocks

        Enchanted current = list.get(0);
        double initY = current.getLocation().getY();
              
        for(int i = 0; i < list.size(); i++)
        {
       	  Location dest = current.getLocation(Direction.EAST, 1.0);			
          current = list.get(i);
          dest.setY(initY + 1f/5*(-i*i + list.size()s*i));
          current.setLocation(dest);
        }
              
    }
}
