<<<<<<< HEAD
=======
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
>>>>>>> a93f2c7eaac326ba9b004107c80ff806e2fc83b5
