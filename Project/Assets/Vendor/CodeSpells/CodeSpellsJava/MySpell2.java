import june.*;

<<<<<<< HEAD
<<<<<<< HEAD
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
=======
public class Shield extends Spell{
=======
public class MySpell2 extends Spell{
>>>>>>> 70d73a6552a4c3e0fa210a4bb002cc9ff32c5c1b
  public void cast(){
    Enchanted en = spawn("redCrateNG", getTarget().getLocation());
    
  }
}
>>>>>>> de4109edcf624e635babfbfe75d37bd8a54108d1
