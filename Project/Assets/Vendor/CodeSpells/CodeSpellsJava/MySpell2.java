import june.*;

public class MySpell2 extends Spell{
  public void cast(){
       Enchanted pond = getByName("Pond");
        Enchanted firePit = getByName("FirePit");
        firePit.grow(10);
        
        EnchantedList objects = firePit.findWithin();
        objects.setLocation(firePit.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(pond.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),pond), 1.0);
        }
  }
}
