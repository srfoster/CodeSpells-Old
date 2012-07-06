import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        print("entered spell");
              Enchanted target = getByName("Player");
        Enchanted rock = getByName("Rock1");
              
              EnchantedList list = target.findLike(rock, 11.0, 10);
        print("has created an enchantedList with "+list.size()+" elements");
              
              Enchanted current = list.get(0);
              double initY = current.getLocation().getY();
              
              for(int i = 0; i < list.size(); i++)
              {
                  Location dest = current.getLocation(Direction.WEST);			
                  current = list.get(i);
                  dest.setY(initY + 1f/5*(-i*i + list.size()*i));
                  current.movement().teleport(dest);
              }
              
              Location bridgeLoc = rock.getLocation(Direction.NORTH);
              list.movement().teleport(bridgeLoc);
        print("has teleported to rock1");

              
    }
}


