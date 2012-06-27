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
    private double objRadius = 2.0;

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
    
    
    public void connectTo(Enchanted enc) {
        /*Implement later
         
         
         */
    }
    
    
    public String commandGlobal (String command) 
    {
        try {
            long before = System.currentTimeMillis();
            out.println(command);
            String response = in.readLine();
            
            System.out.println(response);
			long after = System.currentTimeMillis();
			System.out.println("Ran " + command + " in " + (after-before) + " milliseconds");
            
            return response;
        }
        catch(Exception e) {
            e.printStackTrace();
        }
        return null;
    }

	public String command(String command)
	{
		try{
			long before = System.currentTimeMillis();
			System.out.println("Running " + command);

			String new_command = "";
			if(command.indexOf("$target") > -1)
			{
			 	new_command = command.replaceAll("\\$target","objects[\""+id+"\"]");
			} else {
				new_command = "objects[\""+id+"\"]."+command+";";
			}

			out.println(new_command);

			String response = in.readLine(); //Waits for confirmation from the Unity server...
			System.out.println(response);
			long after = System.currentTimeMillis();
			System.out.println("Ran " + command + " in " + (after-before) + " milliseconds");

			return response;
		}catch(Exception e){
			e.printStackTrace();
		}

		return null;
	}
}
