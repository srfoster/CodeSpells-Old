import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    for(int i=0; i < 10; i++)
      spawn("redCrate", l);
  }
}
