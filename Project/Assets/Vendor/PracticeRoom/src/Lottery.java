import java.util.*;

public class Lottery {
	private static Random randomGenerator = new Random(System.currentTimeMillis());

	/**
	 * Given a list of ticket holders, this method randomly selects 
	 * a winner and returns that ticket holder.
	 * @param ticketHolderList: Our list of ticket holders
	 * @param totalTickets: The total number of tickets available
	 * @return: The winning ticket holder
	 */
	public static <T extends TicketHolder> T getWinner(ArrayList<T> ticketHolderList, int totalTickets)
	{
		// First, get our winning ticket
		int winningTicket = randomGenerator.nextInt(totalTickets);
		int winningIndex = 0;

		int currentTicket = 0;
		T currentHolder;
		
		// Now, go through all items in our ticket holder list, and see if they hold
		// the winning ticket, how this works is we go in order and add the tickets
		// they hold to our currentTicket variable, and then see if that surpasses the
		// winning ticket.
		// So, for example, if our list has the following tickets [1, 2, 4, 10, 1]
		// Picking 1 means index 0 wins.
		// Picking 9 means index 3 wins, because we check:
		// 		1 < 9
		//		1 + 2 = 3 < 9
		// 		1 + 2 + 4 = 7 < 9
		// 		1 + 2 + 4 + 10 = 17 > 9 WINNER!
		while(winningIndex < ticketHolderList.size())
		{
			currentHolder = ticketHolderList.get(winningIndex);
			currentTicket += currentHolder.getTickets();
			if (currentTicket >= winningTicket)
			{
				return currentHolder;
			}
			winningIndex++;
		}
		
		// null means there was no winner, this should never happen...
		return null;
	}
	
	/**
	 * Returns a random element from a given list
	 */
	public static <T> T getRandomElement(ArrayList<T> list)
	{
		return list.get(randomGenerator.nextInt(list.size()));
	}
	
	/**
	 * Returns a random number between the given bounds
	 */
	public static int getRandomNumber(int lowerBound, int upperBound)
	{
		return randomGenerator.nextInt(upperBound-lowerBound) + lowerBound;
	}
}
