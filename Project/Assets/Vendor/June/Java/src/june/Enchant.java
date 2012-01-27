package june;

public class Enchant
{
	public static Enchanted byName(String[] args)
	{
		String id = args[0];
		return new Enchanted(id);
	}	

	public static Enchanted byName(String id)
	{
		return new Enchanted(id);
	}	
}
