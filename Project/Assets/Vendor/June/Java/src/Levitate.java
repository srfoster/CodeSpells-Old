import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
	System.out.println("HERE");
        Log.log("entered spell");
              Enchanted target = getByName("Player");
        Enchanted rock = getByName("Rock1");
        System.out.println("found rock and player");
              
              EnchantedList list = target.findLike(rock, 11.0); //try to get 10 rocks
        Log.log("has created an enchantedList with "+list.size()+" elements");
        int size = (list.size() < 11) ? list.size() : 10;
        System.out.println("got the enchantedlist");
              Enchanted current = list.get(0);
              double initY = current.getLocation().getY();
              
              for(int i = 0; i < size; i++)
              {
                  Location dest = current.getLocation(Direction.EAST);			
                  current = list.get(i);
                  dest.setY(initY + 1f/5*(-i*i + size*i));
                  current.setLocation(dest);
              }
              
              Location bridgeLoc = rock.getLocation(Direction.SOUTH);
        list.setLocation(bridgeLoc);
        Log.log("has teleported to rock1");
        
              
    }
}


