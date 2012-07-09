package june;

import java.io.File;
import java.io.IOException;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Wand
{
	String color;
	long last_datetime;
  File file;

	public Wand(String color)
	{
		this.color = color;
    this.file = new File(color + "_gesture.txt");
	}

	public Direction getGesture()
	{
		String gesture = getGestureString();

    if(gesture == null)
        gesture = "";

    //System.out.println("Gesture was " + gesture);

		if(gesture.equals("Right"))
			return Direction.right();

		if(gesture.equals("Left"))
			return Direction.left();

		return Direction.none();
	}

	private String getGestureString()
	{
      long datetime = file.lastModified();

	    if(datetime == last_datetime)
	    {
		    return "";
	    }

      last_datetime = datetime;

	    return readFromFile(file);
	}

	private String readFromFile(File file)
	{
    try{
      BufferedReader br = new BufferedReader(new FileReader(file));
      
      return br.readLine();
    }catch(FileNotFoundException fnfe){
      System.out.println("File not found");
      return "";
    }catch(IOException fnfe){
      System.out.println("IOException");
      return "";
    }
	}
}
