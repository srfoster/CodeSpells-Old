<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> cfdd7a7313ed65965620d0c92faf1ea4dcc0f1c5
import june.*;

public class MySpell1 extends Spell{
  public void cast(){
<<<<<<< HEAD
    Enchanted a = getByName("Area 1");
    

    EnchantedList l = a.findWithin();

      for(int i = 0; i < 20; i++)
         l.move(Direction.up(), .1);  
   }
}
=======
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
>>>>>>> cfdd7a7313ed65965620d0c92faf1ea4dcc0f1c5
