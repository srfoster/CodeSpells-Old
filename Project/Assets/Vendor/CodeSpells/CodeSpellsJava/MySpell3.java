import june.*;

public class MySpell3 extends Spell{
  public void cast(){
    spawn("redCrate", getTarget().getLocation());
  }
}
