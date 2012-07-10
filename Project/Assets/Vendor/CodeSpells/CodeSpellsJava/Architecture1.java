import june.*;

class Ignite extends Spell{
  public void cast(){
    Enchanted start = getByName("Start");
    Enchanted end   = getByName("End");

    EnchantedList list = getListByName("Rocks");

    Location location = start.getLocation();
    for(Enchanted rock : list)
    {
      Direction direction = Direction.between(start, end); 
      location.adjust(direction);
      rock.setLocation();
    }
  }
}
