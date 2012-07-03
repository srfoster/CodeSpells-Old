import june.*;
import java.util.*;
public class Levitate extends Spell
{
  public void cast()
  {
				

				Enchanted rock1 = getByName("Rock1");
				Enchanted rock2 = getByName("Rock2");
				
				EnchantedList list = rock1.findLike(rock2,4.0);

			for (int i=0; i<list.size();i++) {
					list.get(i).movement().levitate(3.0);
		}
			print("finished levitating");
  }
}
