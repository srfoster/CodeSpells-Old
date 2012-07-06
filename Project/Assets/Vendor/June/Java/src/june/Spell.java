package june;

import june.*;

public abstract class Spell
{
    String target_id;
    private Enchanted ench;
    
  public Spell(){
      ench = new Enchanted("Player");
  }
    
    public void print(String message) {
        ench.commandGlobal("Debug.Log(\""+message+"\")");
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
