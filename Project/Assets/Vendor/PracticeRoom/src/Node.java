import java.util.ArrayList;

/**
 * This is the Node class.
 * @author Haronid
 *
 */
public abstract class Node implements TicketHolder {
	// Fields
	protected Node parent;
	protected String name;
	protected ArrayList<Node> children = new ArrayList<Node>();
	public NodeTypes nodeTypes = new NodeTypes();
	private int liveListTickets = 0;
	private ArrayList<Action> actionTickets = null;
	private int totalActionTickets = 0;
	private boolean hasLeafAsDescendant = false;
	
	// Constructor
	public Node(Node parent, String name)
	{
		this.parent = parent;
		this.name = name;
	}
	
	// Accessors
	public Node getParent()
	{
		return this.parent;
	}
	
	public String getName()
	{
		return this.name;
	}
	
	public ArrayList<Node> getChildren()
	{
		return this.children;
	}
	
	// Building a tree
	protected abstract void PerformRandomAction();
	
	// Mapping to Unity
	public abstract String createObstacle();
	public abstract String createSolution();

	// Accessors and Modifiers
	public void setName(String name) {
		this.name = name;
	}
	
	public void add(Node child)
	{
		children.add(child);
	}

	public int getLiveListTickets() {
		return liveListTickets;
	}

	public void setLiveListTickets(int tickets) {
		this.liveListTickets = tickets;
	}

	public ArrayList<Action> getActionTickets() {
		return actionTickets;
	}

	public void setActionTickets(ArrayList<Action> actionTickets) {
		this.actionTickets = actionTickets;
	}

	public boolean hasLeafAsDescendant() {
		return hasLeafAsDescendant;
	}

	public void setHasLeafAsDescendant(boolean hasLeafAsDescendant) {
		// First, set it for ourselves
		this.hasLeafAsDescendant = hasLeafAsDescendant;
		
		// Now, set it for our parent
		Node parent = this.getParent();
		
		if(parent != null && !parent.hasLeafAsDescendant())
		{
			// This sets it all the way up to the root if needed =)
			parent.setHasLeafAsDescendant(true);
		}
	}

	public int getTotalActionTickets() {
		return totalActionTickets;
	}

	public void setTotalActionTickets(int totalActionTickets) {
		if (totalActionTickets <= 0)
		{
			System.err.println("WHAT ARE YOU DOING?");
		}
		
		this.totalActionTickets = totalActionTickets;
	}
	
	/**
	 * Overriding from TicketHolder, we'll use this in our
	 * LiveList selection
	 */
	@Override
	public int getTickets()
	{
		return getLiveListTickets();
	}
}
