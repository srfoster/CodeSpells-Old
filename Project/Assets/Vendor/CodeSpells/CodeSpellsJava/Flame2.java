import june.*;

public class Flame2 extends Spell{
  public void cast(){
    Enchanted target = getTarget();

    target.onFire(true);
  }
}
