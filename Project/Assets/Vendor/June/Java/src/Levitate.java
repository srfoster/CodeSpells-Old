import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        //rocks to fire pit
        Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("FirePit");
        
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
        
        
        //fire pit to pond
        Enchanted pond = getByName("Pond");
        Enchanted firePit = getByName("FirePit");
        
        for (int i=0; i<30; i++) {
            firePit.scale();
        }
        
        EnchantedList objects = firePit.findWithin();
        for (Enchanted obj: objects) {
            obj.setLocation(firePit.getLocation());
        }
        
        
        while ((objects.getLocation()).distanceBetween(pond.getLocation()) > 1.5) {
            objects.move(Direction.between(objects,pond), 1.0);
        }
        
        
        //pond to bakery
        Enchanted pond = getByName("Pond");
        Enchanted bakery = getByName("Bakery");
        
        for (int i=0; i<45; i++) {
            pond.scale();
        }
        
        EnchantedList objects = pond.findWithin();
        for (Enchanted obj: objects) {
            obj.setLocation(pond.getLocation());
        }
        
        
        while ((objects.getLocation()).distanceBetween(bakery.getLocation()) > 0.8) {
            objects.move(Direction.between(objects,bakery), 0.5);
        }
        

        
        

        
        
        
        
        
        /*        
        while ((objects.getLocaton()).distanceBetween(pond.getLocation()) > 3.5) {
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


