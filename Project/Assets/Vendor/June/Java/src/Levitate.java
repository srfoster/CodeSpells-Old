import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        Log.log("INSIDE LEVITATE");
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
            
            Log.log("new location is x: "+tempLocation.getX()+" y: "+tempLocation.getY()+" z: "+tempLocation.getZ());
            list.get(i).setLocation(tempLocation);
        }
        Log.log("after the loop");
    }
}


