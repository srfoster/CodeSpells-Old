import june.*;

public class Up extends Spell{
  public void cast(){
    Enchanted me = getTarget();
    me.move(Direction.up(), 10.0f);
  }
}
