
public class River extends Leaf {
	
	public River(Node parent, String name) {
		super(parent, name);
		TreeStats.setTickets(this, TreeStats.riverLiveListProbability, TreeStats.riverActionProbability);
	}

	@Override
	public String createObstacle() {
		return "RIVER"+System.getProperty("line.separator");
	}

	@Override
	public String createSolution() {
		return "crossRiver();\n";
	}

	@Override
	public void PerformRandomAction() {
		
	}
}


class SkeletonRiver extends SkeletonNode{

	public SkeletonRiver() {
		super("River", 50);
	}
	@Override
	public Node create(Node parent, String name) {
		return new River(parent, name);
	}
	
}
