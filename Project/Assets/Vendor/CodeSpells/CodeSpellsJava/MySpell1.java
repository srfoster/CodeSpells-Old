import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    //Location tar = getByName("Area 1").getLocation();
    //Enchanted newCrate = spawn("redCrate", tar);
    getTarget().move(Direction.up(), 30.0f);
  }
}
