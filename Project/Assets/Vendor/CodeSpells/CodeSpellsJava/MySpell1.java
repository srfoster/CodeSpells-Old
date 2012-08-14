import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted area1 = getTarget();
    area1.grow(40.0);
    for (int i=0; i<30; i++) {
      spawn("redCrate", area1.getLocation()).grow(2.0);
      }
  }
}
