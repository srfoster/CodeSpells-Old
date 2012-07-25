import june.*;

public class MySpell2 extends Spell{
  public void cast(){
        Enchanted pond = getByName("Area 2");
        Enchanted firePit = getByName("Area 1");
        firePit.grow(10);
        
        EnchantedList objects = firePit.findWithin();
        
        for (Enchanted e : objects) {
            boolean isRock = e.isRock();
            boolean isIgnited = e.isIgnited();
            Log.log("isRock: "+isRock+" isIgnited: "+isIgnited);
            if (!(e.isRock() && e.isIgnited())) {
                objects.remove(e);
            }
        }
        
        
        objects.setLocation(firePit.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(pond.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),pond), 2.0);
        }
  }
}
