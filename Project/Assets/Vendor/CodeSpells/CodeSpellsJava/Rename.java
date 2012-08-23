import june.*;

public class Rename extends Spell{
  public void cast(){
    getTarget().setName("FlamingRock");
  }
}
