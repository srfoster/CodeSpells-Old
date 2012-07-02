import june.*;
import java.util.*;
public class Levitate extends Spell
{
  public void cast()
  {
     EnchantedList list = new EnchantedList();;

   		for(int i = 1; i <=10; i++)
     {
			   Enchanted rock = getByName("Rock" + i);
				  list.add(rock);
     }

     Enchanted last = list.get(0);

     for(int i = 1; i < list.size(); i++)
     {
    		 Enchanted current = list.get(i);
        Location dest = last.getLocation();
        dest.setX(dest.getX() + 2f);
						current.movement().teleport(dest);
        last = current;  
     }

     list.movement().levitate(1f, 100f);
  }
}
