import june.*;

public class MySpell1 extends Spell{
  public void cast(){
    while(true)
    {
      getTarget().grow(-.1);  
    }
  }
}
