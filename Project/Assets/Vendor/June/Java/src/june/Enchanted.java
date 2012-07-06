package june;

import java.io.BufferedReader;
import java.io.PrintWriter;

public class Enchanted
{
	private String id;
	private Movement movement;

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

	public Movement movement()
	{
		if(movement == null)
			movement = new Movement(this);		


		return movement;
	}
    
    
    public static String commandGlobal (String command) 
    {
        try {
            long before = System.currentTimeMillis();
            Log.log("Java sends to Unity: "+command+"\n");
            out.println(command);
            String response = in.readLine();
            Log.log("Java gets back from Unity: "+response);
            
            long after = System.currentTimeMillis();
            
            return response;
        }
        catch(Exception e) {
            e.printStackTrace();
            Log.log("Error in command: " + e);

        }
        return null;
    }

	public String command(String command)
	{
		try{

			long before = System.currentTimeMillis();

			String new_command = "";
			if(command.indexOf("$target") > -1)
			{
			 	new_command = command.replaceAll("\\$target","objects[\""+id+"\"]");
			} else {
				new_command = "objects[\""+id+"\"]."+command+";";
			}
      Log.log("Java about to block, sending to Unity: "+new_command);
			out.println(new_command);
      Log.log("Java sent to Unity");


      Log.log("Java about to block, reading from Unity");
			String response = in.readLine(); //Waits for confirmation from the Unity server...
            
			long after = System.currentTimeMillis();
      Log.log("Java gets back from Unity (in "+(after-before)+" ms): "+response);


			return response;
		}catch(Exception e){
			e.printStackTrace();
      Log.log("Error in command: " + e);
		}

		return null;
	}




    //The actual API
  
    public Location getLocation() {
        return new Location(command("transform.position"));
    }

    public Location getLocation(int dir) {
       Location loc = getLocation();
       loc.adjust(dir);

       return loc; 
    }

    public void setLocation(Location loc)
    {
        String command = "";
        command += "$target.transform.position.x = " + loc.getX() + ";";
        command += "$target.transform.position.y = " + loc.getY() + ";";
        command += "$target.transform.position.z = " + loc.getZ() + ";";

        command(command);
    }
    
    public EnchantedList findLike(Enchanted ench, double rad) {
        System.out.println("inside findLike method");
        String list = commandGlobal("util.getObjWith(\""+this.getId()+"\",\""+ench.getId()+"\","+rad+")");
        System.out.println("got a string back from unity");
        System.out.println("the string is "+list);
        EnchantedList eList = new EnchantedList();
        if (!list.equals("")) {
            String[] ids = list.split(";");
            for (String t : ids) {
                eList.add(new Enchanted(t)); //create new enchanted instance
            }
        }
        return eList;
    }
    
  public void connectTo(Enchanted enc) {
  }
    
  public void move(int dir)
  {
    Location loc = getLocation(dir);
    setLocation(loc);
  }

	public double currentHeight()
	{
		return Double.parseDouble(command("$target.transform.position.y - Terrain.activeTerrain.SampleHeight($target.transform.position)"));
	}
}
