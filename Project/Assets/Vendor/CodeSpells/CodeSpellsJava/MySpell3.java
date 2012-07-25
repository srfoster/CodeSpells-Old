import june.*;

public class MySpell3 extends Spell{
  public void cast(){
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
