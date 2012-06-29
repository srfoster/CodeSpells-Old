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
        super.commandGlobal("Debug.Log(\"before I instantiate\")");
        super.commandGlobal("var empty = new GameObject(); empty.AddComponent(Rigidbody); empty.rigidbody.isKinematic = true; empty = util.instantiate (empty, Vector3(0,0,0), Quaternion.identity); objects.Add(\""+super.getId()+"\", empty); ");
        super.commandGlobal("Debug.Log(\"after I instantiate\")");
        eList = new ArrayList<Enchanted>();
        setEmptyPos = false;
    }
    
    public void add(Enchanted ench) {
        //gets parent game object
        if (!setEmptyPos) {
            super.commandGlobal("objects[\""+super.getId()+"\"].transform.position = objects[\""+ench.getId()+"\"].transform.position");
            setEmptyPos = true;
        }
        super.commandGlobal("Debug.Log(\"inside the add method\")");
        super.commandGlobal("objects[\""+ench.getId()+"\"].transform.parent = objects[\""+super.getId()+"\"].transform");
        eList.add(ench);
    }
    
    public void buildBridge() {
        
        boolean isOdd = (eList.size()%2 == 1);
        int middleIndex = isOdd ? (eList.size()/2) : (eList.size()/2-1);
        double tempHeight = 0.0;
        double heightChange = 0.5;
        //determine the height change based on the length of the list (work on later)
        
        for(int i=0; i < eList.size(); i++) {
            super.commandGlobal("objects[\""+eList.get(i).getId()+"\"].transform.position.y += tempHeight");
            if (i < middleIndex) {
                tempHeight += heightChange;
            }
            else if (i == middleIndex) {
                if (!isOdd) middleIndex += 1;
            }
            else
                tempHeight -= heightChange;

        }
        
    }
    
    //public void remove(Enchanted ench) {
        
    //}
    
}