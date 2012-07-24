import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted me = getByName("Me");
    Enchanted area = getByName("Area 1");
    
    
    me.setLocation(area.getLocation());
  }
}
