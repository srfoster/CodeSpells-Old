import june.*;

public class MakeCrate extends Spell{
  public void cast(){
    Location l = getTarget().getLocation();
    
    Enchanted e = spawn("redCrate",l);
  }
}
