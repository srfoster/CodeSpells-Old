import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
			Enchanted entity = getTarget();
			//Enchanted rock1 = getByName("Rock1");
			
			EnchantedList myList = new EnchantedList();
			//myList.add(rock1);
			myList.add(entity);
			Location my_location = myList.getLocation(new Direction(Direction.west()));
			entity.movement().teleport(my_location);
			myList.movement().forward(4.0f);
			
  }
}

