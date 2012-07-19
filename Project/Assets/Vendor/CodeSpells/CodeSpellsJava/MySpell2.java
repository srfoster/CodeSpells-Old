import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    Enchanted t = getTarget();
    
    while(true)
      t.scale();
  }
}
