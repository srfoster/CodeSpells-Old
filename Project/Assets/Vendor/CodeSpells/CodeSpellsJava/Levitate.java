import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
    Enchanted entity1 = getTarget();
    Enchanted entity2 = getByName("My Plant");

    entity1.movement().levitate(10f,20f);
    entity2.movement().levitate(10f,20f);
  }
}
