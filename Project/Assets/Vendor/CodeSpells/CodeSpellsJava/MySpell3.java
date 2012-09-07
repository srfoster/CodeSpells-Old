import june.*;

public class Lay extends Spell{
  public void cast(){
    Enchanted me = getByName("Me");

    
    while(true){
      spawn("redCrateNG", me.getLocation());
      
      try{
        Thread.sleep(1000);
      } catch(Exception e) {
        break;
      }
    }
  }
}
