import june.*;

public class MySpell3 extends Spell{
  public void cast(){
        Enchanted pond = getByName("Pond");
        Enchanted bakery = getByName("Bakery");
        pond.grow(45);
        
        EnchantedList objects = pond.findWithin();
        objects.setLocation(pond.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(bakery.getLocation()) > 0.8) {
            objects.move(Direction.between(objects.get(0),bakery), 0.5);
        }
  }
}
