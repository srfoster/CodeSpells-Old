import june.*;

public class Follow extends Spell{
  public void cast(){
    Enchanted e = getByName("Me");
    
    while(true)
      getByName("Area 1").setLocation(me.getLocation());
  }
}
