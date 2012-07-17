import june.*;
import java.util.*;

public class Summon1 extends Spell{
  public void cast(){
    Enchanted rain    = getByName("Rain");
    Enchanted myself  = getByName("Me");

    while(rain.distanceBetween(myself) > 65){
      Direction toward_me = Direction.between(rain,myself);
      rain.move(toward_me, 5);
    }
  }
}
