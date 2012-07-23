import june.*;

public class MySpell1 extends Spell{
  public void cast(){
<<<<<<< HEAD
           Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("FirePit");
        startRegion.grow(10);
        
        EnchantedList objects = startRegion.findWithin();
        objects.setLocation(startRegion.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(firePit.getLocation()) > 3.5) {
            objects.move(Direction.between(firePit,objects.get(0)), 2.0);
        }
=======
    //Do magic here.
>>>>>>> fc8ac34b1ec07bb21ea0f3a247ae7a6f8d0c5bbb
  }
}
