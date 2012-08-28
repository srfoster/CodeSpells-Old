import june.*;

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
        myCrate1.move(Direction.up(), 10);
        
        myCrate2.setLocation(myLoc);
        myCrate2.move(Direction.forward(), 10);
        
        myCrate3.setLocation(myLoc);
        myCrate3.move(Direction.backward(), 10);
        
        myCrate4.setLocation(myLoc);
        myCrate4.move(Direction.left(), 10);
      
        myCrate5.setLocation(myLoc);
        myCrate5.move(Direction.right(), 10);
      }
  }
}
