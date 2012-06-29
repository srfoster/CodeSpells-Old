import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
<<<<<<< HEAD
			Enchanted entity = getTarget();
			//Enchanted rock1 = getByName("Rock1");
			
			EnchantedList myList = new EnchantedList();
			//myList.add(rock1);
			myList.add(entity);
=======
     Enchanted fountain = getTarget();
     Wand green_wand = new Wand("green");
>>>>>>> 2ebf50452c5ff23ced3039dd0d27ef45dc5bd003

			Location my_location = myList.getLocation(new Direction(Direction.west()));
			entity.movement().teleport(my_location);
			myList.movement().forward(4.0f);
			
  }
}

