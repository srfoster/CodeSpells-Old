import june.*;

public class Sentry1 extends Spell{
  public void cast(){
    Enchanted thing = getTarget();
    
    while(true){
      moveEast(thing);
      moveWest(thing);
    }
  }
  
  public void moveEast(Enchanted e){
    for(int i = 0; i <100; i++)
    {
      e.move(Direction.left(),.2f);
    } 
  } 
  
  public void moveWest(Enchanted e){
    for(int i = 0; i <100; i++)
    {
      e.move(Direction.right(),.2f);
    } 
  } 
}

