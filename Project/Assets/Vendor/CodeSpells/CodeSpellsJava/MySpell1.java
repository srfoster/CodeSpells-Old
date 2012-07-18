import june.*;

public class MySpell1 extends Spell{
  public void cast(){
<<<<<<< HEAD
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
=======
    //Do magic here.
>>>>>>> ed82d753d67f3fc8f48809f3bfb058823df22ef6
  }
}
