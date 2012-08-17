import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    for (int i = 0; i < 50; i++)
      spawn("redCrate" , l);
  }
}
