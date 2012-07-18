import june.*;

public class MySpell2 extends Spell{
  public void cast(){
            Enchanted startRegion = getTarget();
        Enchanted firePit = getByName("firePit");
        Enchanted pond = getByName("pond");

        for (int i=0; i<30; i++) {
            startRegion.scale();
        }
        EnchantedList objects = startRegion.findWithin();
        for (Enchanted obj: objects) {
            obj.setLocation(startRegion.getLocation());
        }
        
        objects.move(Direction.between(objects,firePit), 6.0);
  }
}
