import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    getTarget().move(Direction.north(), 20);
  }
}
