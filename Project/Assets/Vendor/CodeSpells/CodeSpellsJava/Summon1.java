import june.*;
import java.util.*;

public class Summon1 extends Spell{
  public void cast(){
    Enchanted target  = getTarget();
    Enchanted crate   = getByName("MyCrate");
    Enchanted me      = getByName("Client");
    
    crate.setLocation(me.getLocation());

    while(crate.distanceTo(target) > 1){
      Direction d = Direction.between(crate,target);
      crate.move(d, 1);
    }
  }
}
