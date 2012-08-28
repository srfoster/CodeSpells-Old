import june.*;

public class MySpell2 extends Spell{
  public void cast(){
    Enchanted en = spawn("blueCrateNG", getTarget().getLocation());
en.setName("myCrate");
    en.move(Direction.up(), 25);
          
    Enchanted myself = getByName("Me");

    Location temp = en.getLocation();
    Location dest = temp.adjust(Direction.up(), 10);

    myself.setLocation(dest);

  }
}