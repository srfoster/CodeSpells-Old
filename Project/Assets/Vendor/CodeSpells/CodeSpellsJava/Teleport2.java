import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    for(int i=0; i<30; i++) {
      spawn("blueCrate", getTarget().getLocation());
      
      }
  }
}