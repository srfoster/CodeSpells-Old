import june.*;

public class Levitate
{
  public static void main(String[] args)
  {
    Enchanted entity = Enchant.byName(args);

    entity.movement().levitate(4.0);
  }
}