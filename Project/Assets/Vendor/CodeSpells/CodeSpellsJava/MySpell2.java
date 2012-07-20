import june.*;

public class MySpell2 extends Spell{
  public void cast(){
<<<<<<< HEAD
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
=======
    Enchanted t = getTarget();
    
    while(true)
      t.scale();
>>>>>>> 6dbb00acab63976213dfa08aa6f69ae458b0d323
  }
}
