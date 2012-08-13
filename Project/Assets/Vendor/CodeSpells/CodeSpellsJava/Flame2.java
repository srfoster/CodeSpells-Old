import june.*;

public class Flame2 extends Spell{
  public void cast(){
    Enchanted thing = getTarget();

    thing.onFire(true);
  }
}
