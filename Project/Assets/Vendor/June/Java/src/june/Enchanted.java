package june;

import java.io.BufferedReader;
import java.io.PrintWriter;

public class Enchanted
{
	private String id;

	static PrintWriter out;
	static BufferedReader in;

	/**
	 * A new Enchanted -- which is a binding to a game entity in Unity.
	 *
	 * @param id The unity id of the game object controlled by this instance.
	 */
	public Enchanted(String id)
	{
    out = UnityConnection.getOutgoingWriter();
    in  = UnityConnection.getIncomingReader();
    
		this.id = id;
	}

	public String getId()
	{
		return id;
	}
    
  public void setId(String temp)
	{
		id = temp;
	}	

  public void setName(String new_name)
  {
    commandGlobal("util.reregister(\""+id+"\",\""+new_name+"\");");
    setId(id);
  }
    
    
    public static String commandGlobal (String command) 
    {
        try {
            long before = System.currentTimeMillis();
            Log.log("Java sends to Unity: "+command+"\n");
            out.println(command);
            String response = in.readLine();

            if(response.startsWith("Error"))
            {
              throw new RuntimeException("Unity Error");
            }

            Log.log("Java gets back from Unity: "+response);

            
            long after = System.currentTimeMillis();
            
            return response;
        }
        catch(Exception e) {
            e.printStackTrace();
            Log.log("Error in command: " + e);

            System.exit(1);
        }
        return null;
    }

	public String command(String command)
	{
      String new_command = "";
			if(command.indexOf("$target") > -1)
			{
			 	new_command = command.replaceAll("\\$target","objects[\""+id+"\"]");
			} else {
				new_command = "objects[\""+id+"\"]."+command+";";
			}

      return commandGlobal(new_command);
	}




    //The actual API
  
    public Location getLocation() {
        return new LazyLocation("objects['"+getId()+"'].transform.position");
    }

    public Location getLocation(Direction dir, double scale) {
       Location loc = getLocation();
       loc.adjust(dir, scale);

       return loc; 
    }

    public void adjustLocation(Vector3 loc){
        String command = "";
        command += "$target.transform.position.x += " + loc.getXString() + ";";
        command += "$target.transform.position.y += " + loc.getYString() + ";";
        command += "$target.transform.position.z += " + loc.getZString() + ";";

        command(command);
    }

    public void setLocation(Location loc)
    {
        String command = "";
        command += "$target.transform.position.x = " + loc.getXString() + ";";
        command += "$target.transform.position.y = " + loc.getYString() + ";";
        command += "$target.transform.position.z = " + loc.getZString() + ";";

        command(command);
    }

    public EnchantedList findWithin()
    {
        String list = commandGlobal("util.getWithin(\""+this.getId()+"\")");
        EnchantedList eList = new EnchantedList();
        eList.addAllFromUnityString(list);
        return eList;
    }
    
    public EnchantedList findLike(Enchanted ench, double rad) {
        String list = commandGlobal("util.getObjWith(\""+this.getId()+"\",\""+ench.getId()+"\","+rad+")");
        EnchantedList eList = new EnchantedList();
        eList.addAllFromUnityString(list);
        return eList;
    }
    

    public void scale() {
        command("$target.transform.localScale = $target.transform.localScale + (new Vector3(0.2,0.2,0.2))");
    }
    public double distanceTo(Enchanted ench) {
        return Double.parseDouble(command("Vector3.Distance($target.transform.position , objects[\""+ench.getId()+"\"].transform.position)"));
    }
    
    
  public void move(Direction dir, double speed)
  {
    dir.times(speed);

    adjustLocation(dir);
  }

	public double currentHeight()
	{
		return Double.parseDouble(command("$target.transform.position.y - Terrain.activeTerrain.SampleHeight($target.transform.position)"));
	}

  public void onFire(boolean bool)
  {
    if(bool)
    {
      command("GetComponent('Flamable').Ignite()");
    } else {
      command("GetComponent('Flamable').Extinguish()");
    }
  }
}
