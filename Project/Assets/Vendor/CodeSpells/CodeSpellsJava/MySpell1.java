import june.*;
 
public class MySpell1 extends Spell{
  public void cast(){
    Enchanted e = getByName("Area 1");
    Enchanted me = getByName("Me");
    EnchantedList list = new EnchantedList();
 
    for(int i = 0; i < 30; i++)
    {
      Enchanted c = spawn("blueCrate", me.getLocation());
      list.add(c);
    }
 
 
    while(true){
      Direction dir = Direction.forward();
 
      Vector3 dest = me.getLocation();
 
      int x = 70;
      for(Enchanted c : list)
      {
        dest = dest.add(dir.times(x));
 
        c.setLocation((Location)dest);
        x += 10;
      }
    }
  }
}
