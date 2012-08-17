import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    spawn("redCrate" , l);
  }
}
