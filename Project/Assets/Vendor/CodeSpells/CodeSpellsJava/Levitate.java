import june.*;
import java.util.*;
public class Levitate extends Spell
{
  public void cast()
  {
<<<<<<< HEAD
				EnchantedList list = new EnchantedList();

				Enchanted rock1 = getByName("Rock1");
				Enchanted rock2 = getByName("Rock2");
				Enchanted rock3 = getByName("Rock3");
				Enchanted rock4 = getByName("Rock4");
				Enchanted rock5 = getByName("Rock5");
				Enchanted rock6 = getByName("Rock6");

				for(int i = 1; i <=6; i++)
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
	float len = (float) list.size();

	dest.setY(dest.getY() + (-i*i + len*x));

	current.movement().teleport(dest);
       last = current;
    }

    list.movement().levitate(10f, 100f);
				
=======
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
>>>>>>> fe48ef8734d0ebac8f2946e6403cd0e3b5b0b8e3
  }
}
