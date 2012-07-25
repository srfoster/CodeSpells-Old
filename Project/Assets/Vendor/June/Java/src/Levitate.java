import june.*;
import java.util.*;
      
public class Levitate extends Spell
{
    public void cast()
    { 
        //rocks to fire pit
        Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("Area 1");
        startRegion.grow(10);
        
        EnchantedList objects = startRegion.findWithin();
        
        Log.log("before for loop");
        
        for (Enchanted e : objects) {
            if (!(e.isRock())) {
                objects.remove(e);
            }
        }
        
        Log.log("after for loop");
        
        objects.setLocation(startRegion.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(firePit.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),firePit), 2.0);
        }
        
        
        //fire pit to pond
        Enchanted pond = getByName("Area 2");
        Enchanted firePit = getByName("Area 1");
        firePit.grow(10);
        
        EnchantedList objects = firePit.findWithin();
        
        for (Enchanted e : objects) {
            boolean isRock = e.isRock();
            boolean isIgnited = e.isIgnited();
            Log.log("isRock: "+isRock+" isIgnited: "+isIgnited);
            if (!(e.isRock() && e.isIgnited())) {
                objects.remove(e);
            }
        }
        
        
        objects.setLocation(firePit.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(pond.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),pond), 2.0);
        }
        
        
        //pond to bakery
        Enchanted pond = getByName("Area 2");
        Enchanted bakery = getByName("Area 3");
        pond.grow(15);
        
        EnchantedList objects = pond.findWithin();
                
        for (Enchanted e : objects) {
            if (!(e.isRockSugar() && !e.isIgnited())) {
                objects.remove(e);
            }
        }                
                
        objects.setLocation(pond.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(bakery.getLocation()) > 0.8) {
            objects.move(Direction.between(objects.get(0),bakery), 0.5);
        }
        
        
        
        
        
        
        
        
    }
}


