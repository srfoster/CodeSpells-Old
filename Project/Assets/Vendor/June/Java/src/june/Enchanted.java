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

	public String command(String command)
	{
		try{
			long before = System.currentTimeMillis();
			System.out.println("Running " + command);
			out.println("objects[\""+id+"\"]."+command+";");

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
