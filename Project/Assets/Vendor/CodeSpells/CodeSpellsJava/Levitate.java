import june.*;

public class Levitate extends Spell
{
  public static void main(String[] args)
  {
    Enchanted entity1 = Enchant.byTarget(args);
    Enchanted entity2 = Enchant.byName("Fountain");

    entity1.movement().levitate(10f,20f);
    entity2.movement().levitate(10f,20f);
  }
}
