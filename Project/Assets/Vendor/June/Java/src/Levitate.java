import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        System.out.println("INSIDE LEVITATE");
        Enchanted destination = getByName("Destination");
        Enchanted start = getByName("Rock1");
        Enchanted center = getByName("Rock2");
        
        System.out.println("about to get an enchantedlist");
        EnchantedList list = center.findLike(center, 11.0);
        Log.log("should have gotten enchantedlist");
        

        Direction direction = Direction.between(start,destination);
        Location tempLocation = start.getLocation();
        int size = (list.size() < 11) ? list.size() : 10;
        
        
        System.out.println("before for loop");
        //make iterable next
        for(int i = 0; i < size; i++)
        {
            tempLocation.adjust(direction, 1.0);
            //Log.log("new location is x: "+tempLocation.getX()+" y: "+tempLocation.getY()+" z: "+tempLocation.getZ());
            list.get(i).setLocation(tempLocation);
        }
        System.out.println("after the loop");
    }
}


