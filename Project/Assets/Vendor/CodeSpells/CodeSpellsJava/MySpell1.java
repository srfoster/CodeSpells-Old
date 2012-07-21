import june.*;

public class MySpell1 extends Spell{
  public void cast(){
           Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("FirePit");
        startRegion.grow(10);
        
        EnchantedList objects = startRegion.findWithin();
        objects.setLocation(startRegion.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(firePit.getLocation()) > 3.5) {
            objects.move(Direction.between(firePit,objects.get(0)), 2.0);
        }
  }
}
