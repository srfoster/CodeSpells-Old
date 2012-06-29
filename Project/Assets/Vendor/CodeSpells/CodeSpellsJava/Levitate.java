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
  }
}
