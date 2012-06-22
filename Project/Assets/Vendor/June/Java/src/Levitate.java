import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
    Enchanted target = getTarget();
    Enchanted fountain = getByName("Fountain");

    fountain.movement().levitate(10f,20f);
    target.movement().levitate(10f,20f);
  }
}
