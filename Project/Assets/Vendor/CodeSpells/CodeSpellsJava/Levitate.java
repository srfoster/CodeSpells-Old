import june.*;

public class Levitate
{
  public static void main(String[] args)
  {
    Enchanted entity = Enchant.byName(args);

    while(true)
    {
      entity.movement().levitate(10.0f);
    }
  }
}
