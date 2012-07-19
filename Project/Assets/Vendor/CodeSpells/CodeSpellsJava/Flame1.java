import june.*;

public class Flame1 extends Spell{
  public void cast(){
    Enchanted thing = getTarget();

    thing.onFire(false);
  }
}
