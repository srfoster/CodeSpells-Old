package june;

import june.*;
//import sun.misc.Signal;
//import sun.misc.SignalHandler;
import java.util.Observer;
import java.util.Observable;

public class Caster // implements Observer
{
//   private void startSigHandler() {
//     
//   }
  
  public static void main(String[] args) throws Exception
  {
    //startSigHandler();
//     SignalHandler handler = new SignalHandler() {
//         public void handle(Signal sig) {
//             Log.log("Termination signal caught... throwing exception.");
//             throw new RuntimeException("Termination signal");
//         }
//     };
//     Signal.handle(new Signal("TERM"), handler);
    
    Spell spell = (Spell) Class.forName(args[0]).newInstance();
    spell.setupSigHandler();
    // final SigHandler sh = new SigHandler();
//      sh.addObserver( spell );
//      sh.handleSignal( "HUP" );
    
    spell.setTarget(args[1]);
    
    EventLog.logEvent("\n\nCasting\n"+args[0]);
    
    (new Enchanted("")).executeCommand("util.log(\""+(args[0]).trim()+"\")");

    try{
        spell.cast();
        (new Enchanted("")).executeCommand("util.endCast(\""+(args[0]).trim()+"\")");
        spell.sendObjectListToUnity();
    }catch(Exception e){
        //(new Enchanted("")).executeCommand("util.endCast(\""+(args[0]).trim()+"\")");
      e.printStackTrace();

      (new Enchanted("")).executeCommand("util.popup('"+e.getClass() + "\\n" + e.getMessage()+"')");
    }
    //(new Enchanted("")).executeCommand("util.endCast(\""+(args[0]).trim()+"\")");
  }
  
//   public void update( final Observable o, final Object arg )
//  {
// // use the same method that the Timer employs to trigger a
//  // rotation, which ensures that signal and timer don't screw
//  // each other up.
//  //System.out.println( "Received signal: "+arg );
//  Log.log("Received signal: "+arg);
//     System.exit(1);
//  }
}
