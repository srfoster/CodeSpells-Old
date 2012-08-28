import june.*;

public class Tower extends Spell{
  public void cast(){
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
  }
}