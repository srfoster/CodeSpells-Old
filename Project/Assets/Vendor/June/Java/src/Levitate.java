import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        //rocks to fire pit
        Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("FirePit");
        startRegion.grow(30);
        
        EnchantedList objects = startRegion.findWithin();
        objects.setLocation(startRegion.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(firePit.getLocation()) > 3.5) {
            objects.move(Direction.between(objects.get(0),firePit), 2.0);
        }
        
        
        //fire pit to pond
        Enchanted pond = getByName("Pond");
        Enchanted firePit = getByName("FirePit");
        firePit.grow(30);
        
        EnchantedList objects = firePit.findWithin();
        objects.setLocation(firePit.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(pond.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),pond), 1.0);
        }
        
        
        //pond to bakery
        Enchanted pond = getByName("Pond");
        Enchanted bakery = getByName("Bakery");
        pond.grow(45);
        
        EnchantedList objects = pond.findWithin();
        objects.setLocation(pond.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(bakery.getLocation()) > 0.8) {
            objects.move(Direction.between(objects.get(0),bakery), 0.5);
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


