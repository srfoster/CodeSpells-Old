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

			Location my_location = myList.getLocation(Direction.WEST);
			entity.movement().teleport(my_location);
			myList.movement().forward(4.0f);
			
=======
     Enchanted fountain = getTarget();
     Wand green_wand = new Wand("green");

     while(true)
     {
        if(green_wand.getGesture() == Direction.RIGHT)
            fountain.movement().right(1f);
        else if(green_wand.getGesture() == Direction.LEFT)
            fountain.movement().left(1f);
    
     }
>>>>>>> 2ebf50452c5ff23ced3039dd0d27ef45dc5bd003
  }
}
