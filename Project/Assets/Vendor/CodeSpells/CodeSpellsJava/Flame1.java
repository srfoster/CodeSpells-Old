import june.*;

public class Flame1 extends Spell{
  public void cast(){
    Enchanted thing = getByName("Rock1");

    thing.onFire(false);
  }
}
