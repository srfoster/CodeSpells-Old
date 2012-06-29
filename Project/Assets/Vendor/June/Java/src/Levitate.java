import june.*;

public class Levitate extends Spell
{
  public void cast()
  {
     Enchanted fountain = getTarget();
     Wand green_wand = new Wand("green");

     while(true)
     {
        if(green_wand.getGesture() == Direction.RIGHT)
            fountain.movement().right(1f);
        else if(green_wand.getGesture() == Direction.LEFT)
            fountain.movement().left(1f);
     }

     /*
     Location exact = getLocation();
     Location right = getLocation(Direction.right());
     Location left  = getLocation(Direction.left());

     Enchanted fountain = getByName("Fountain");
     Enchanted plant   = getByName("Plant");
     Enchanted rock    = getByName("Rock");

     fountain.movement().teleport(right);
     rock.movement().teleport(left);
     plant.movement().teleport(exact);
     */
  }
}
