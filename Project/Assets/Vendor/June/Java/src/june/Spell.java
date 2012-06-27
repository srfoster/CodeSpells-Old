package june;

import june.*;

public abstract class Spell
{
    String target_id;
    private Enchanted ench;
    
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
    
    public Location getLocation(Direction dir) {
        if(dir.getDirection() == Direction.LEFT) //left
        {
            String vector3_string = ench.commandGlobal("transform.position - transform.right");
            
            System.out.println(vector3_string);
            return null;// Location(vector3_string);
        }
        if(dir.getDirection() == Direction.RIGHT) {//right
            
        }
        else {
           // ench.getLocation(dir);
        }
     
        return null;
    }
    
    
    public Location getLocation() 
    {
        double x = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.x"));
        double y = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.y"));
        double z = Double.parseDouble(ench.commandGlobal("GameObject.Find (\"Spawning Zone\").transform.position.z"));
        return (new Location (x,y,z));
    }
    

  public abstract void cast();
}
