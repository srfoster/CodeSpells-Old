package june;

import june.*;

public class Caster
{
  public static void main(String[] args) throws Exception
  {
    Spell spell = (Spell) Class.forName(args[0]).newInstance();
    spell.setTarget(args[1]);


    try{
        spell.cast();
    }catch(Exception e){
      e.printStackTrace();

      (new Enchanted("")).executeCommand("util.popup('"+e.getClass() + "\\n" + e.getMessage()+"')");
    }
  }
}
