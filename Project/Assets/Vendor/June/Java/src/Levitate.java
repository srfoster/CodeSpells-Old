import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        Enchanted destination = getByName("Destination");
        Enchanted start = getByName("Rock1");
        Enchanted center = getByName("Rock2");
        
        EnchantedList list = center.findLike(center, 11.0);
        Direction direction = Direction.between(start,destination);
        Location tempLocation = start.getLocation();
        int size = (list.size() < 11) ? list.size() : 10;
        
        //make iterable next
        for(int i = 0; i < size; i++)
        {
            tempLocation.adjust(direction, 1.0);
            list.get(i).setLocation(tempLocation);
        }
        
        
        
        
	/*System.out.println("HERE");
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
        */
              
    }
}


