package june;

import java.util.*;

public class EnchantedList extends Enchanted implements Iterable<Enchanted>
{
    //list of enchanted objects
    //add method
    private ArrayList<Enchanted> eList;
    private boolean setEmptyPos;
    
    public EnchantedList() {
        //creates empty gameobject, and names it.
        super("");


        Random r = new Random();
        long rand = r.nextLong();

        super.setId((""+this.hashCode())+rand);
        super.commandGlobal("empty = new GameObject(); empty = util.instantiate (empty, Vector3(0,0,0), Quaternion.identity); empty.name =\""+this.hashCode()+"\"; empty.layer=2; objects.Add(\""+super.getId()+"\", empty); ");
        eList = new ArrayList<Enchanted>();
        setEmptyPos = false;
    }

    public EnchantedList(String id)
    {
       super(id);

       eList = new ArrayList<Enchanted>();

       String list = commandGlobal("util.getEnchantedChildrenOf(\""+this.getId()+"\")");
       setEmptyPos = true;
       addAllFromUnityString(list);
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
        //gets parent game object
        if (!setEmptyPos) {
            super.commandGlobal("objects[\""+super.getId()+"\"].transform.position = objects[\""+ench.getId()+"\"].transform.position");
            setEmptyPos = true;
        }
        super.commandGlobal("objects[\""+ench.getId()+"\"].transform.parent = objects[\""+super.getId()+"\"].transform");
        eList.add(ench);
    }
    
    //public void remove(Enchanted ench) {
        
    //}
    
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
}
