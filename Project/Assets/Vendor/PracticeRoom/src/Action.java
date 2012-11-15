
public class Action implements TicketHolder {
	private int actionTickets;
	private int actionId;
	
	public Action(int id, int tickets)
	{
		actionId = id;
		setActionTickets(tickets);
	}

	public int getActionTickets() {
		return actionTickets;
	}

	public void setActionTickets(int actionTickets) {
		this.actionTickets = actionTickets;
	}
	
	public int getActionId() {
		return actionId;
	}

	/**
	 * Overriding from TicketHolder, we'll use this to
	 * pick a random action
	 */
	@Override
	public int getTickets() {
		return getActionTickets();
	}
}
