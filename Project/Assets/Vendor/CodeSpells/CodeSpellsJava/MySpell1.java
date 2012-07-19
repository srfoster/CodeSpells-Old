import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted a = getByName("Area 1");
    

    EnchantedList l = a.findWithin();

      for(int i = 0; i < 20; i++)
         l.move(Direction.up(), .1);  
   }
}
