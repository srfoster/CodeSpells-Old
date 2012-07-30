import june.*;

public class Sentry2 extends Spell{
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
      e.move(Direction.east(),.2f);
    } 
  } 
  
  public void moveWest(Enchanted e){
    for(int i = 0; i <100; i++)
    {
      e.move(Direction.west(),.2f);
    } 
  } 
}

