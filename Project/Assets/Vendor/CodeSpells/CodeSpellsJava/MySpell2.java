import june.*;

<<<<<<< HEAD
public class MySpell2 extends Spell{
  public void cast(){
Enchanted myCrate = spawn("redCrate", getTarget().getLocation());
Location myLoc = getByName("Me").getLocation();
while(true) {
myCrate.setLocation(myLoc);
myCrate.move(Direction.forward(), 10);
  }
}
 } 
=======
public class Follow extends Spell{
  public void cast(){
    Enchanted e = getByName("Me");
    
    while(true)
      getByName("Area 1").setLocation(me.getLocation());
  }
}
>>>>>>> a93f2c7eaac326ba9b004107c80ff806e2fc83b5
