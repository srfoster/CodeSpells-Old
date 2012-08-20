import june.*;

public class Create extends Spell{
  public void cast(){
    Location loc = getTarget().getLocation();
    for(int i=0; i<25; i++) {
      Enchanted temp = spawn("blueCrate", loc);
      temp.grow(0.7);
      temp.move(Direction.up(), 30.0f);
      }
  }
}
