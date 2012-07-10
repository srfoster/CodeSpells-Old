package june;

import java.util.ArrayList;

public class EnchantedList extends Enchanted 
{
    //list of enchanted objects
    //add method
    private ArrayList<Enchanted> eList;
    private boolean setEmptyPos;
    
    public EnchantedList() {
        //creates empty gameobject, and names it.
        super("");
        super.setId(""+this.hashCode());
        super.commandGlobal("empty = new GameObject(); empty = util.instantiate (empty, Vector3(0,0,0), Quaternion.identity); empty.layer=2; objects.Add(\""+super.getId()+"\", empty); ");
        eList = new ArrayList<Enchanted>();
        setEmptyPos = false;
    }

    public EnchantedList(String id)
    {
       super(id);

       String list = commandGlobal("util.getObjWith(\""+this.getId()+"\",\""+ench.getId()+"\","+rad+")");
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
    
    
    public Enchanted get(int index)
    {
      return eList.get(index);
    }

    public int size()
    {
      return eList.size();
    }
}
