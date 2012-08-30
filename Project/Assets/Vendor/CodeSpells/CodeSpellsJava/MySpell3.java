import june.*;

public class Tower extends Spell{
  public void cast(){
<<<<<<< HEAD
<<<<<<< HEAD
    Enchanted crate = getTarget();
    Location cLoc = crate.getLocation();
    Direction up = Direction.up();
    cLoc.add(up);
    spawn("redCrate", cLoc);
=======
    spawn("redCrate", getTarget().getLocation());
>>>>>>> de4109edcf624e635babfbfe75d37bd8a54108d1
=======
    Enchanted e = getByName("Area 1");
    Enchanted me = getByName("Me");
    EnchantedList list = new EnchantedList();


    Vector3 dest = e.getLocation();
    for(int i = 0; i < 10; i++)
    {
      Direction dir = Direction.up();

      dest = dest.add(dir.times(i*2.5));

      Enchanted c = spawn("redCrate", (Location)dest);
      list.add(c);
    }
>>>>>>> 422fb8412fe4776f4955a0e7c59cf5481a933be5
  }
}
