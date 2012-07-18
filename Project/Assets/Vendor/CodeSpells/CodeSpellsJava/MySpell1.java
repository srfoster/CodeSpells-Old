import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted a1 = getByName("Area 1");
    Enchanted a2 = getByName("Area 2");

    Location loc1 = a1.getLocation();
    loc1.adjust(Direction.up(), 10);
    Location loc2 = a2.getLocation();
    loc2.adjust(Direction.up(), 10);

    while(true){
        EnchantedList list1 = a2.findWithin();
        EnchantedList list2 = a1.findWithin();


        for(Enchanted thing : list1)
        {
            thing.setLocation(loc2);
        }

        for(Enchanted thing : list2)
        {
            thing.setLocation(loc1);
        }
    }
  }
}
