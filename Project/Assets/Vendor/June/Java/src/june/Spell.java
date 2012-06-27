package june;

import june.*;

public abstract class Spell
{
  String target_id;
    Enchanted ench;
    
  public Spell(){
      ench = new Enchanted("");
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
    
    //gets spawning zone location
    
    /*public Location getLocation(Direction dir) {
        if(dir.getDirection() == 5) {//left
            
        }
        if(dir.getDirection() == 6) {//right
            
        }
        else {
            ench.getLocation(dir);
        }
        
    }*/
    
    
    public Location getMyLocation() 
    {
        double x = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.x"));
        double y = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.y"));
        double z = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.z"));
        return (new Location (x,y,z));
    }
    

  public abstract void cast();
}
