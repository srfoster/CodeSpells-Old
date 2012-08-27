import june.*;

public class Shoot extends Spell{
  public void cast(){
    Enchanted me = getByName("Me");
    getTarget().setLocation(me.getLocation());
    
    Direction dir = Direction.forward();
    dir.freeze();    
    
    while(true)
    {
       getTarget().move(dir,1);
       getTarget().grow(1);
    }
  }
}
