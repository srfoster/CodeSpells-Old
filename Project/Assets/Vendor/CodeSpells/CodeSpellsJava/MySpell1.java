import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted area = getByName("Area 1");


    for(int i = 0; i < 10; i++)
      area.scale();

    EnchantedList list = area.findWithin();


    for(Enchanted rock : list)
    {
      rock.onFire(true);
    }
  }
}
