import june.*;

public class MySpelll extends Spell{
  public void cast(){
    Enchanted tar = getTarget();
    tar.onFire(true);
    tar.move(Direction.west(), 2.0f);
  }
}
