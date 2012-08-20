import june.*;

public class Fly extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    for(int i=0; i < 10; i++)
      spawn("redCrate", l);
  }
}
