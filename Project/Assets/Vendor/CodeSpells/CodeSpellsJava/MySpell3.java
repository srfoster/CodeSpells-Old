import june.*;

public class MySpell3 extends Spell{
  public void cast(){
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

  }
}
