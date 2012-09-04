import june.*;
 
public class TrickyShoot extends Spell{
  public void cast(){
<<<<<<< HEAD
     Enchanted my_target = getByName("myCrate");
    Enchanted me = getByName("Me");
    EnchantedList all = new EnchantedList();


    for(int i=0; i<29; i++) {
      all.add(spawn("redCrate", my_target.getLocation()));
      }

    int counter = 0;
    while(counter < 30)
    {
      my_target.move(Direction.up(), 0.1);
      counter = counter + 1;
    }
        int count=0;
    while(true && my_target.distanceTo(me) < 10){
        count++;
       my_target.move(Direction.forward(), 0.2);
      if (count%20==0) {
        for(Enchanted e:all) {
        e.setLocation(my_target.getLocation());
          }
        }
    }
=======
    Enchanted me = getByName("Me");
 
    Enchanted c1 = spawn("redCrate", me.getLocation());
 
    double i = 0;
    while(true){
      double x = Math.sin(i)*20;
 
      Direction dir = Direction.forward();
 
      Vector3 dest = me.getLocation();
      dest = dest.add(dir.times(25+x));
 
      c1.setLocation((Location)dest);
 
      i++;
    }
  }
}
<<<<<<< HEAD
>>>>>>> 422fb8412fe4776f4955a0e7c59cf5481a933be5
=======
>>>>>>> 36fb2109e75ee725ae5ca250c4235c32b501a9b6
