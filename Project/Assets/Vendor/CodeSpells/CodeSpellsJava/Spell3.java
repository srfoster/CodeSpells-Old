import june.*;

public class ManyCrates extends Spell{
  public void cast(){
    int currentNum = 1;
    Enchanted massiveArea = getTarget();
    massiveArea.grow(400);
EnchantedList list = massiveArea.findWithin();

    while(true) {
      String name;
      int tempNum;
EnchantedList tempList = massiveArea.findWithin();
      for (Enchanted e : tempList) {
        name = e.getId();
        if ((name.length() > 5) && 
        name.substring(0,5).equals("Area ")) {
          tempNum = Integer.parseInt(name.substring(6));
          if (tempNum > currentNum) {
		currentNum = tempNum;
          }
          }
      }
list.setLocation(getByName("Area "+currentNum).getLocation());
     }
  }
}