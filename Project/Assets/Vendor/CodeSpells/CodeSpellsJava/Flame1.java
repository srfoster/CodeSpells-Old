import june.*;

public class Flame1 extends Spell{
  public void cast(){
    Enchanted target = getByName("Rock");

    target.onFire(true);
  }
}
