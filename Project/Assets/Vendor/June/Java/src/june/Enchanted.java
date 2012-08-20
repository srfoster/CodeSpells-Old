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
    
  protected void setId(String temp)
	{
		id = temp;
	}	

  public void setName(String new_name)
  {
    //executeCommand("util.reregister(\""+id+"\",\""+new_name+"\");");
    //
    String command = "$target.GetComponent('Enchantable').setId('"+new_name+"')";
    command = interpolateId(command);
    executeCommand(command); 

    setId(new_name);
  }
    
    
    public static String executeCommand(String command) 
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

	private String interpolateId(String command)
	{
      String new_command = "";
			if(command.indexOf("$target") > -1)
			{
			 	new_command = command.replaceAll("\\$target","objects[\""+id+"\"]");
			} else {
				new_command = "objects[\""+id+"\"]."+command+";";
			}

      return new_command;
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

    public void move(Direction dir, double speed)
    {
      executeCommand(moveCommand(dir,speed));
    }
    
    public String moveCommand(Direction dir, double speed)
    {
      Vector3 new_dir = dir.times(speed);

      return adjustLocationCommand(new_dir);
    }

    public void adjustLocation(Vector3 loc)
    {
       executeCommand(adjustLocationCommand(loc));
    }

    public String adjustLocationCommand(Vector3 loc){
        String command = "";
        command += "$target.transform.position.x += " + loc.getXString() + ";";
        command += "$target.transform.position.y += " + loc.getYString() + ";";
        command += "$target.transform.position.z += " + loc.getZString() + ";";

        String c = interpolateId(command);
        return c;
    }

    public String setLocationCommand(Location loc)
    {
        String command = "";
        command += "$target.transform.position.x = " + loc.getXString() + ";";
        command += "$target.transform.position.y = " + loc.getYString() + ";";
        command += "$target.transform.position.z = " + loc.getZString() + ";";

        String c = interpolateId(command);
        return c;
    }
    
    public void setLocation(Location loc)
    {
        executeCommand(setLocationCommand(loc));
    }

    public EnchantedList findWithin()
    {
        String list = executeCommand("util.getWithin(\""+this.getId()+"\")");
        EnchantedList eList = new EnchantedList();
        eList.addAllFromUnityString(list);
        return eList;
    }
    
    /*
     GameObject sample_crate_prefab = Resources.Load("sample_crate") as GameObject;
     
     Network.Instantiate(sample_crate_prefab, transform.position,
     Quaternion.identity, 0);
     
     */
    
    public boolean isRock() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+1+")"));
    }
    
    public boolean isPlant() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+2+")"));
        
    }
    
    public boolean isSeed() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+3+")"));
    }
    
    public boolean isRockSugar() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+4+")"));
    }
    
    public boolean isFlour() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+5+")"));
    }
    
    public boolean isBread() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+6+")"));
    }
    /*
     public boolean isFlag() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+7+")"));
     
     }
     */
    
    public boolean isIgnited() {
        return Boolean.parseBoolean(executeCommand("util.isOfType(\""+this.getId()+"\","+7+")"));
    }

    public String growCommand(double amount) {
        String c = interpolateId("$target.transform.localScale = $target.transform.localScale + (new Vector3("+amount+","+amount+","+amount+"))");
        return c;
    }

    public void grow(double amount)
    {
        String temp = growCommand(amount);
        //Log.log("To be sent to executeCommand: "+temp);
        executeCommand(temp);
      //executeCommand(growCommand(amount));
    }

    public double distanceTo(Enchanted ench) {
        String c = interpolateId("Vector3.Distance($target.transform.position , objects[\""+ench.getId()+"\"].transform.position)");
        String resp = executeCommand(c);
        return Double.parseDouble(resp);
    }
    
  public double sizeX()
  {
      String response = executeCommand("util.size('"+getId()+"')");

      double ret = Double.parseDouble(response.split(",")[0]);

      return ret;
  }

  public double sizeY()
  {
      String response = executeCommand("util.size('"+getId()+"')");

      double ret = Double.parseDouble(response.split(",")[1]);

      return ret;
  }

  public double sizeZ()
  {
      String response = executeCommand("util.size('"+getId()+"')");

      double ret = Double.parseDouble(response.split(",")[2]);

      return ret;
  }

  public String onFireCommand(boolean bool)
  {
    String c = "";
    if(bool)
    {
      c = interpolateId("GetComponent('Flamable').Ignite()");
    } else {
      c = interpolateId("GetComponent('Flamable').Extinguish()");
    }

    return c;
  }

  public void onFire(boolean bool)
  {
    String c = onFireCommand(bool);
    executeCommand(c);
  }
}
