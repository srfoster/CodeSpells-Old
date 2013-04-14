package june;

import june.*;

public abstract class PracticeSpell extends Spell
{
  public PracticeSpell(){
      super();
  }
    
    
  public void CrossRiver()
  {
        
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
    return Enchant.byName(target_id); 
  }

  protected Enchanted getByName(String name)
  {
    return Enchant.byName(name);
  }

  public abstract void cast();
}
