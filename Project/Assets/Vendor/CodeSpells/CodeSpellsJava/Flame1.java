import june.*;

public class Flame1 extends Spell{
  public void cast(){
<<<<<<< HEAD
while(true) {
        Enchanted obj = getTarget();
        obj.scale();
}
=======
    Enchanted target = getByName("Rock");

    target.onFire(true);
>>>>>>> c8fbe9984f4909a59d0a7394d57a6a9e380b0332
  }
}
