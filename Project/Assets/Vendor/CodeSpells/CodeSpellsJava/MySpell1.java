import june.*;

public class MySpell1 extends Spell{
  public void cast(){
        Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("Area 1");
        startRegion.grow(10);
        
        EnchantedList objects = startRegion.findWithin();
        
        Log.log("before for loop");
        
        for (Enchanted e : objects) {
            if (!(e.isRock())) {
                objects.remove(e);
            }
        }
        
        Log.log("after for loop");
        
        objects.setLocation(startRegion.getLocation());
        
        while ((objects.get(0).getLocation()).distanceBetween(firePit.getLocation()) > 1.5) {
            objects.move(Direction.between(objects.get(0),firePit), 2.0);
        }
  }
}
