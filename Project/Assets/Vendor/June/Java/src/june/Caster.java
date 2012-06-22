package june;

import june.*;

public class Caster
{
  public static void main(String[] args) throws Exception
  {
    Spell spell = (Spell) Class.forName(args[0]).newInstance();
    spell.setTarget(args[1]);
    spell.cast();
  }
}
