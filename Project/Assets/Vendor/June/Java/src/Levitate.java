import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        //Enchanted me = getByName("Me");
        Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("FirePit");
        Enchanted pond = getByName("Pond");

        for (int i=0; i<30; i++) {
            startRegion.scale();
        }
        EnchantedList objects = startRegion.findWithin();
        for (Enchanted obj: objects) {
            obj.setLocation(startRegion.getLocation());
        }
        
        while ((objects.getLocation()).distanceBetween(firePit.getLocation()) > 3.5) {
            objects.move(Direction.between(objects,firePit), 2.0);
         }
        EnchantedList objects2 = firePit.findWithin();
        objects2.setLocation(startRegion.getLocation());
        
        
        /*while((objects2.getLocation()).distanceBetween(me.getLocation()) > 3.5) {
            objects2.move(Direction.between(objects2,me), 2.0);
        }*/
        
        
        
        
        /*        
        while ((objects.getLocation()).distanceBetween(pond.getLocation()) > 3.5) {
            objects.move(Direction.between(objects,pond), 2.0);
        }
        
        objects = pond.findWithin();
        me.setLocation(pond.getLocation());
        objects.move(Direction.up(), 10);*/
        
        


        
        
        
        //When I first get the flag region, I will move each item to the center one by one
        //then move the enchanted list and keep the flag region in the same place
        
        
        /*origLoc.freeze();

        
        while((center.getLocation()).distanceBetween(origLoc) > 3.5) {
            center.move(Direction.between(center.getLocation(),origLoc), 2.0);
        }*/
        
        
        /*        while ((center.getLocation()).distanceBetween(pond.getLocation()) > 3.5) {
         center.move(Direction.between(center,pond), 2.0);
         }
 
         */
        
    }
}


