import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
    Enchanted entity = getTarget();
			
			Location my_location = getMyLocation();
			entity.movement().teleport(my_location);
  }
}
