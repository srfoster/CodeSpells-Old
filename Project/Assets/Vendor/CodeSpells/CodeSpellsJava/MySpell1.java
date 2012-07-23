import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    while(true){
      getByName("Area 1").move(Direction.up(), .5);  
    }
  }
}
