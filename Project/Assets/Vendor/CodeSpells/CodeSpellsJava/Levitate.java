	import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
    Enchanted entity = getTarget();
			Enchanted rock1 = getByName("Rock1");

			EnchantedList myList = new EnchantedList();
			myList.add(rock1);
			myList.add(entity);

			Location my_location = entity.getLocation(new Direction(Direction.west()));
			myList.movement().teleport(my_location);
			//myList.movement().forward(4.0f);
			
			
  }
}
