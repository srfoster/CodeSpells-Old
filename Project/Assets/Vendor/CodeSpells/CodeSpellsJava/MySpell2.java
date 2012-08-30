import june.*;

public class MySpell2 extends Spell{
  public void cast(){
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