import june.*;

public class CrateSpell extends Spell{
  public void cast(){
Enchanted flag1 = getByName("Area 1");
    Enchanted flag2 = getByName("Area 2");
Enchanted crate1 = spawn("redCrate", flag1.getLocation());
Enchanted crate2 = spawn("blueCrate", flag2.getLocation());
    crate1.grow(5.0);
    }
}
