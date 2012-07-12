import june.*;

public class Flame1 extends Spell{
  public void cast(){
        Enchanted destination = getByName("Dest");
        Enchanted center = getTarget();
        
        while (center.distanceBetween(destination) > 2.5) {
            center.move(Direction.between(center,destination), 2.0);
        }
  }
}
