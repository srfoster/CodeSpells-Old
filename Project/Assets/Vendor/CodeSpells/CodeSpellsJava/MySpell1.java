import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    Enchanted e = getTarget();
    getByName("Area 1").grow(300);

    EnchantedList list = getByName("Area 1").findWithin();

    set(list.get(0), e, Direction.north());
    set(list.get(1), e, Direction.south());
    set(list.get(2), e, Direction.east());
    set(list.get(3), e, Direction.west());
  }

  public void set(Enchanted crate, Enchanted target, Direction d)
  {
    Vector3 dest = target.getLocation();
    dest = dest.add(d.times(crate.sizeX()));

    crate.setLocation((Location)dest);
  }
}
