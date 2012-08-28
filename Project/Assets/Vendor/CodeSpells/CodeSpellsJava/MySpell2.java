import june.*;

public class MySpell2 extends Spell{
  public void cast(){
       Enchanted e = getByName("Area 1");
   Enchanted me = getByName("Me");

   Enchanted c1 = spawn("redCrate", e.getLocation());
   Enchanted c2 = spawn("redCrate", e.getLocation());
   Enchanted c3 = spawn("redCrate", e.getLocation());
   Enchanted c4 = spawn("redCrate", e.getLocation());
   Enchanted c5 = spawn("redCrate", e.getLocation());

   while(true){
     Direction dir = Direction.forward();

     Vector3 dest = me.getLocation();
     dest = dest.add(dir.times(10));

     c1.setLocation((Location)dest);
  }
}
 } 
