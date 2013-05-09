package june;

import june.*;
import java.util.*;

public abstract class Spell implements Observer
{
    String target_id;
    private Enchanted ench;
    private ArrayList<Enchanted> objectList = new ArrayList<Enchanted>();
    
  public Spell(){
  }
  
  public void setupSigHandler() {
    final SigHandler sh = new SigHandler();
    sh.addObserver( this );
    sh.handleSignal( "TERM" );
  }
    
  public Enchanted spawn(String prefabName, Location targetLocation) {
      String vstring = targetLocation.toString();
      String name = Enchanted.executeCommand("util.spawnOverNetwork(\""+prefabName+"\", "+vstring+")");
      return (new Enchanted(name));
  }

  public void setTarget(String target_id)
  {
    this.target_id = target_id;
  } 

  protected Enchanted getTarget(){
    Enchanted e = Enchant.byName(target_id);
    addObject(e);
    return  e;
  }

  protected Enchanted getByName(String name)
  {
    Enchanted e = Enchant.byName(name);
    addObject(e);
    return e;
  }
  
  private void addObject(Enchanted e) {
    if (!e.getId().equals(""))
        objectList.add(e);
  }
  
  public void sendObjectListToUnity() {
    for (int i = 0; i < objectList.size(); i++) {
        Enchanted obj = objectList.get(i);
        Enchanted.executeCommand("util.logObj(\""+obj.getId()+"\")");
	 }
  }
  
  public void update( final Observable o, final Object arg )
 {
    // use the same method that the Timer employs to trigger a
     // rotation, which ensures that signal and timer don't screw
     // each other up.
     //System.out.println( "Received signal: "+arg );
     Log.log("Received signal: "+arg);
     Enchanted.executeCommand("util.endCast(\""+this.getClass().getName()+"\")");
     sendObjectListToUnity();
     System.exit(1);
 }

  public abstract void cast();
}
