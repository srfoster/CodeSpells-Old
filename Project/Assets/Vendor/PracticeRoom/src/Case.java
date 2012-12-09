
public class Case<T> extends Branch {
	public T value;
	
	public Case(Node parent, String name, T newVal) {
		super(parent, name);
		this.value = newVal;
		TreeStats.setTickets(this, TreeStats.caseLiveListProbability, TreeStats.caseActionProbability);
		this.nodeTypes.changeProbability(50, 50	, 0, 0); // Probability for picking certain nodes as children
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
		for(Node child : this.children)
		{
			solution.append(child.createSolution());
		}
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

/**
 * Note: For now, no Skeleton Class, because Case nodes shouldn't really be picked as part of AddChild...
 * Just like there's no SkeletonRoot...
 */