import june.*;

public class Flame1 extends Spell{
  public void cast(){
<<<<<<< HEAD
<<<<<<< HEAD
while(true) {
        Enchanted obj = getTarget();
        obj.scale();
}
=======
    Enchanted target = getByName("Rock");
=======
    Enchanted target = getTarget();
>>>>>>> 7f4cafb7093ea620a09fdf20611ff91a67e2c69e

    target.onFire(true);
>>>>>>> c8fbe9984f4909a59d0a7394d57a6a9e380b0332
  }
}
