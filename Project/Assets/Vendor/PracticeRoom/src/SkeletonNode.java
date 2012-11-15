
public abstract class SkeletonNode implements TicketHolder {
	private String baseName = "BaseName";
	private int creatingMeTickets = -1;
	
	public SkeletonNode(String name, int creatingMeTickets) {
		baseName = name;
		this.creatingMeTickets = creatingMeTickets;
	}
	
	public abstract Node create(Node parent, String name);

	public String getBaseName() {
		return baseName;
	}

	public void setBaseName(String baseName) {
		this.baseName = baseName;
	}

	public int getCreatingMeTickets() {
		return creatingMeTickets;
	}

	public void setCreatingMeTickets(int creatingMeTickets) {
		this.creatingMeTickets = creatingMeTickets;
	}
	
	/**
	 * Overriding from TicketHolder, we'll use this for 
	 * picking a child
	 */
	@Override
	public int getTickets()
	{
		return getCreatingMeTickets();
	}
}
