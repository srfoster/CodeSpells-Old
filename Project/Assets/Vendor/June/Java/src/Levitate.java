import june.*;
import java.util.*;
      
      public class Levitate extends Spell
      {
          public void cast()
          { 
              Enchanted mc = getByName("Player");
              Enchanted rock = getTarget();
              EnchantedList list = mc.findLike(rock, 4.0); 
              
              
              for (int i=0; i<list.size();i++) {
                  list.get(i).movement().levitate(5.0, 0.2);
                  print("has levitated object");
              }            
              
              
          }
      }


