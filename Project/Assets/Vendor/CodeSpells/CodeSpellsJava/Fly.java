import june.*;

public class Fly extends Spell{
  public void cast(){
    Enchanted me = getByName("Me");
    boolean isUp = false;
      me.move(Direction.up(),30.0f);
    while(true) {
      if(isUp) {
        me.move(Direction.up(), 5.0f);
        }
      else {
        me.move(Direction.down(), 5.0f);
        }
      }

  }
}
