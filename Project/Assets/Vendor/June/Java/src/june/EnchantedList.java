package june;

import java.util.*;

import java.io.BufferedReader;
import java.io.PrintWriter;


public class EnchantedList implements Iterable<Enchanted>
{
    //list of enchanted objects
    //add method
    private ArrayList<Enchanted> eList;
    private boolean setEmptyPos;

    PrintWriter out;
    BufferedReader in;
    
    public EnchantedList() {
        out = UnityConnection.getOutgoingWriter();
        in  = UnityConnection.getIncomingReader();

        eList = new ArrayList<Enchanted>();
    }

    public void addAllFromUnityString(String list)
    {
       if (!list.equals("")) {
         String[] ids = list.split(";");
         for (String t : ids) {
           add(new Enchanted(t)); //create new enchanted instance
         }
       }
    }
    
    public void add(Enchanted ench) {
        eList.add(ench);
    }
    
    public void remove(Enchanted ench) {
        int index = 0;
        for (Enchanted e : eList) {
            if ((e.getId()).equals(ench.getId())) {
                eList.remove(index);
                Log.log("I have just removed object with Id: "+ench.getId());
            }
            index++;
        }
    }
    
    public Enchanted get(int index)
    {
      return eList.get(index);
    }

    public int size()
    {
      return eList.size();
    }

    public Iterator<Enchanted> iterator()
    {
      return eList.iterator();
    }

    //The API
    
    public void onFire(boolean bool)
    {
       String big_command = "";

       for(Enchanted e : eList)
       {
          big_command += e.onFireCommand(bool) + ";";
       }

       Enchanted.executeCommand(big_command);
    }

    public void move(Direction dir, double speed)
    {
       String big_command = "";

       for(Enchanted e : eList)
       {
          big_command += e.moveCommand(dir, speed) + ";";
       }

       Enchanted.executeCommand(big_command);
    }

    public void grow(double amount)
    {
       String big_command = "";

       for(Enchanted e : eList)
       {
          big_command += e.growCommand(amount) + ";";
       }

       Enchanted.executeCommand(big_command);
    }

    public void setLocation(Location loc)
    {
       String big_command = "";

       for(Enchanted e : eList)
       {
          big_command += e.setLocationCommand(loc) + ";";
       }

       Enchanted.executeCommand(big_command);
    }

}
