import june.*;

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
