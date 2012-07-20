import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted target = getTarget();
    target.grow(30);
  }
}
