import june.*;

public class Levitate
{
  public static void main(String[] args)
  {
    Enchanted entity = Enchant.byName(args);

    entity.movement().levitate(4.0);
    for(int i = 1; i <= 100; i++)
	    entity.movement().forward(.03);
    for(int i = 1; i <= 100; i++)
	    entity.movement().right(.03);
    for(int i = 1; i <= 100; i++)
	    entity.movement().backward(.03);
    for(int i = 1; i <= 100; i++)
	    entity.movement().left(.03);

    entity.movement().drop();
  }
}
