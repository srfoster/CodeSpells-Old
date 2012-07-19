<<<<<<< HEAD
import june.*;

public class MySpell1 extends Spell{
  public void cast(){
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
  }
}
=======
>>>>>>> 6dbb00acab63976213dfa08aa6f69ae458b0d323
