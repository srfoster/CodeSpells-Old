import june.*;

public class ManyCrates extends Spell{
  public void cast(){
    int currentNum = 1;
    Enchanted massiveArea = getTarget();
    massiveArea.grow(400);
EnchantedList list = massiveArea.findWithin();
list.setLocation(getByName("Area "+currentNum).getLocation());

  }
}