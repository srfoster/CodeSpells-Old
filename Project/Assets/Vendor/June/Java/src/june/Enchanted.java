package june;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;

import java.net.Socket;


public class Enchanted
{
	private String id;
	private Movement movement;

	Socket soc;
	PrintWriter out;
	BufferedReader in;
    
    /*public enum Direction {
        NORTH, SOUTH, EAST, WEST
    }*/
    private double objRadius = 1.0;

	/**
	 * A new Enchanted -- which is a binding to a game entity in Unity.
	 *
	 * @param id The unity id of the game object controlled by this instance.
	 */
	public Enchanted(String id)
	{
		try{
			soc = new Socket("127.0.0.1",3000);
			out = new PrintWriter(soc.getOutputStream(), true);
			in = new BufferedReader(new InputStreamReader(soc.getInputStream()));
		}catch(Exception e){
			e.printStackTrace();
			//Should also probably tell Unity that there's been a problem...
		}

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
    
    public Location getLocation() {
        double x = Double.parseDouble(command("transform.position.x"));
        double y = Double.parseDouble(command("transform.position.y"));
        double z = Double.parseDouble(command("transform.position.z"));
        return (new Location(x,y,z));
    }
    

    
    
    public Location getLocation(int dir) {
        Location myLoc = getLocation();
        double xVal = myLoc.getX();
        double zVal = myLoc.getZ();
        
        if(dir == Direction.NORTH) xVal += objRadius;//north
        if(dir == Direction.SOUTH) xVal -= objRadius;//south
        if(dir == Direction.EAST) zVal -= objRadius;//east
        if(dir == Direction.WEST) zVal += objRadius;//west
        
        return (new Location(xVal, myLoc.getY(), zVal));
    }
    
    public EnchantedList findLike(Enchanted ench, double rad) {
        String list = commandGlobal("util.getObjWith(\""+this.getId()+"\",\""+ench.getId()+"\","+rad+")");
        EnchantedList eList = new EnchantedList();
        if (!list.equals("")) {
            String[] ids = list.split(";");
            for (String t : ids) {
                eList.add(new Enchanted(t)); //create new enchanted instance
            }
        }
        return eList;
    }
    
    public EnchantedList findLike(Enchanted ench, double rad, int maxNum) {
        String list = commandGlobal("util.getObjWith(\""+this.getId()+"\",\""+ench.getId()+"\","+rad+","+maxNum+")");
        
        //Trying to eval 'util.getObjWith("Rock1","Rock1",8.0",10)
        
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
        /*Implement later
         
         
         */
    }
    
    
    public String commandGlobal (String command) 
    {
        try {
            long before = System.currentTimeMillis();
            Log.log("Java sends to Unity: "+command);
            out.println(command);
            String response = in.readLine();
            Log.log("Java gets back from Unity: "+response);
            
            System.out.println(response);
			long after = System.currentTimeMillis();
			System.out.println("Ran " + command + " in " + (after-before) + " milliseconds");
            
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
        Log.log("Entered command()");
		try{
            Log.log("Entered command() try/catch");

			long before = System.currentTimeMillis();
            Log.log("Got system time");

            Log.log("About to tweak command");

			String new_command = "";
			if(command.indexOf("$target") > -1)
			{
                Log.log("Replacing $target with object[id]");
			 	new_command = command.replaceAll("\\$target","objects[\""+id+"\"]");
			} else {
                Log.log("Appending object[id]");
				new_command = "objects[\""+id+"\"]."+command+";";
			}
            Log.log("Java sends to Unity: "+new_command);
			out.println(new_command);

			String response = in.readLine(); //Waits for confirmation from the Unity server...
            
			long after = System.currentTimeMillis();
			System.out.println("Ran " + command + " in " + (after-before) + " milliseconds");
            Log.log("Java gets back from Unity (in "+(after-before)+" ms): "+response);


			return response;
		}catch(Exception e){
			e.printStackTrace();
            Log.log("Error in command: " + e);
		}

		return null;
	}
}
