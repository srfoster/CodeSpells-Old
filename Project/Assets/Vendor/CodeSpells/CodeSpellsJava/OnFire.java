import june.*;

public class OnFire extends Spell{
  public void cast(){
    Enchanted e = getTarget();
    e.move(Direction.south(), 4.0f);
  }
}
