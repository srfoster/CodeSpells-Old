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
    
    //gets spawning zone location
    
    public Location getLocation(int dir) {
        if(dir == Direction.LEFT) //left
        {
            String vector3_string = ench.command("$target.transform.position - $target.transform.right * 10");
            
            return new Location(vector3_string);
        }
        if(dir == Direction.RIGHT) {//right
            String vector3_string = ench.command("$target.transform.position + $target.transform.right * 10");
            
            return new Location(vector3_string);
        }
        else {
           ench.getLocation(dir);
        }
     
        return null;
    }
    
    
    public Location getLocation() 
    {
       String vector3_string = ench.command("$target.transform.position");
            
       return new Location(vector3_string);
    }
    

  public abstract void cast();
}
