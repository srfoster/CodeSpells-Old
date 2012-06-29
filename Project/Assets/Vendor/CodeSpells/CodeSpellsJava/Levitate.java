		import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
				EnchantedList myList = new EnchantedList();
				Enchanted entity = getTarget();
				Enchanted rock1 = getByName("Rock1");
				Enchanted rock2 = getByName("Rock2");
				Enchanted rock3 = getByName("Rock3");
				Enchanted rock4 = getByName("Rock4");
				myList.add(entity);
				myList.add(rock1);
				myList.add(rock2);
				myList.add(rock3);
				myList.add(rock4);
				Location my_location = entity.getLocation(Direction.WEST); //west of entity's location
				rock1.movement().teleport(my_location);
				my_location = rock1.getLocation(Direction.WEST);
				rock2.movement().teleport(my_location);
				
				my_location = rock2.getLocation(Direction.WEST);
				rock3.movement().teleport(my_location);
				my_location = rock3.getLocation(Direction.WEST);
				rock4.movement().teleport(my_location);
				myList.buildBridge();
				
  }
}
