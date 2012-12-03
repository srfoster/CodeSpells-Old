
public class Root extends Branch {

	public Root() {
		// Roots have no parent
		super(null, "root");
		TreeStats.setTickets(this, TreeStats.rootLiveListProbability, TreeStats.rootActionProbability);
		this.nodeTypes.changeProbability(10, 10, 0, 80); // Probability for picking certain nodes as children
	}

	@Override
	public String createObstacle() {
		StringBuilder obstacle = new StringBuilder();
		for(Node child : this.children)
		{
			obstacle.append(child.createObstacle());
		}
		return obstacle.toString();
	}

	@Override
	public String createSolution() {
		StringBuilder solution = new StringBuilder();
		solution.append("public static void main(String args []) {\n");
		
		for(Node child : this.children)
		{
			solution.append(child.createSolution());
		}
		
		solution.append("\n}");
		return solution.toString();
	}

	@Override
	public void PerformRandomAction() {
		Action winningAction = Lottery.getWinner(this.getActionTickets(), this.getTotalActionTickets());
		
		switch(winningAction.getActionId())
		{
		case 0:
			AddChild();
			break;
		default: 
			break;
		}
	}
}
