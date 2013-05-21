package june;

import june.*;
import java.util.Observer;
import java.util.Observable;

public class Caster
{
  
  public static void main(String[] args) throws Exception
  {
    
    Spell spell = (Spell) Class.forName(args[0]).newInstance();
    spell.setupSigHandler();
    
    spell.setTarget(args[1]);
    
    EventLog.logEvent("\n\nCasting\n"+args[0]);
    
    try{
        spell.cast();
        spell.sendObjectListToUnity();
    }catch(Exception e){
      e.printStackTrace();

      (new Enchanted("")).executeCommand("util.popup('"+e.getClass() + "\\n" + e.getMessage()+"')");
    }
  }
  
}
