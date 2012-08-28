import june.*;

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
  public void cast(){
    Enchanted myCrate1 = spawn("redCrate",
      getTarget().getLocation());
        Enchanted myCrate2 = spawn("redCrate",
      getTarget().getLocation());
        Enchanted myCrate3 = spawn("redCrate",
      getTarget().getLocation());
        Enchanted myCrate4 = spawn("redCrate",
      getTarget().getLocation());
        Enchanted myCrate5 = spawn("redCrate",
      getTarget().getLocation());
    
    Location myLoc = getByName("Me").getLocation();
    
    while(true){
        myCrate1.setLocation(myLoc);
        myCrate1.move(Direction.up(), 3);
              myCrate2.setLocation(myLoc);
        myCrate2.move(Direction.up(), 3);
              myCrate3.setLocation(myLoc);
        myCrate3.move(Direction.up(), 3);
              myCrate4.setLocation(myLoc);
        myCrate4.move(Direction.up(), 3);
              myCrate5.setLocation(myLoc);
        myCrate5.move(Direction.up(), 3);
      }
  }
}
>>>>>>> de4109edcf624e635babfbfe75d37bd8a54108d1
