import java.util.ArrayList;

public class TreeStats {
	// Set base number of tickets
	private static final int baseLiveListTickets = 10;
	private static final int baseActionTickets = 10;
	
	// Setting default probabilities for different classes
	public static final int riverLiveListProbability = 0;
	public static final int[] riverActionProbability = null;
	
	public static final int wallLiveListProbability = 0;
	public static final int[] wallActionProbability = null;
	
	public static final int loopLiveListProbability = 20;
	public static final int[] loopActionProbability = new int[]{3,1};
	
	public static final int conditionalLiveListProbability = 20;
	public static final int[] conditionalActionProbability = new int[]{1};
	
	public static final int caseLiveListProbability = 30;
	public static final int[] caseActionProbability = new int[]{1};
	
	public static final int rootLiveListProbability = 10;
	public static final int[] rootActionProbability = new int[]{1};
	
	
	/**
	 * Sets actionTickets and liveListTickets for the given Node
	 * 
	 * @param node 
	 * The node we want to set the tickets (probabilities) for 
	 * @param probability
	 * Probability of being chosen in the live list
	 * @param actionProbabilities
	 * Array of probabilities for each node's specific actions
	 * 
	 * @remark Default probabilities are set through each node's constructor
	 */
	public static void setTickets(Node node, int probability, int[] actionProbabilities)
	{	
		// Make sure they actually have actions to set probabilities for
		if(actionProbabilities != null)
		{	
			int totalActionTickets = 0;
			int currentActionTickets = 0;
			ArrayList<Action> actions = new ArrayList<Action>();
			
			// Go through all actions and multiply by base number of tickets
			for(int i=0; i<actionProbabilities.length; i++)
			{	
				currentActionTickets = actionProbabilities[i] * baseActionTickets;
				
				// Keep track of total action tickets this node has
				totalActionTickets += currentActionTickets;
				
				// Add this action to our action list
				actions.add(new Action(i, currentActionTickets));
			}
			
			node.setActionTickets(actions);
			node.setTotalActionTickets(totalActionTickets);
		}
		
		node.setLiveListTickets((probability*baseLiveListTickets));
	}
}
