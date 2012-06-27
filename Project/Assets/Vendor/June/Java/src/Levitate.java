import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
    Enchanted entity = getTarget();
    Enchanted other = getByName("Planter");

    Location my_location = other.getLocation(Direction.WEST);
    entity.movement().teleport(my_location);
  }
}
