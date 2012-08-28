import june.*;

public class MySpell1 extends Spell{
  public void cast(){
       EnchantedList all = new EnchantedList();
    Location loc = getTarget().getLocation();
    Enchanted me = getByName("Me");
 
    for(int i=0; i < 30; i++)
    {
      Enchanted e = spawn("redCrate", loc);
      all.add(e);
    }
 
    while(true)
    {
      Direction dir = Direction.forward();
 
      Vector3 dest = me.getLocation();
      dest = dest.add(dir.times(20));
 
      for(Enchanted e : all)
      {
        e.setLocation((Location)dest);
      }
    }
  }
}