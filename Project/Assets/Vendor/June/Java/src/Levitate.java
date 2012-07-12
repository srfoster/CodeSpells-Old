import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        Enchanted destination = getByName("Dest");
        Enchanted center = getTarget();
        
        while (center.distanceBetween(destination) > 2.5) {
            center.move(Direction.between(center,destination), 2.0);
        }
        
        
        
        //EnchantedList list = center.findLike(center, 25.0);
        
        /*for(int i = 0; i < 1; i++) {
            Enchanted currentRock = list.get(i);
            currentRock.move(Direction.between(currentRock,destination), 2.0);
        }*/
        
        /*Direction direction = Direction.between(start,destination);

        Location last = start.getLocation();
        
        
        for(int i = 0; i < list.size(); i++)
        {
            last.add(direction);
            list.get(i).setLocation(last);
            last = list.get(i).getLocation();
        }*/

    }
}


