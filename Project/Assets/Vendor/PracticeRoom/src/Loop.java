
public class Loop extends Branch {
	public int iterations = 3;
	public static int minIterations = 2;
	public static int maxIterations = 5;
	
	public Loop(Node parent, String name) {
		super(parent, name);
		TreeStats.setTickets(this, TreeStats.loopLiveListProbability, TreeStats.loopActionProbability);
		this.nodeTypes.changeProbability(0, 100	, 0); // Probability for picking certain nodes as children
	}

	@Override
	public String createObstacle() {
		StringBuilder obstacle = new StringBuilder();
		for(int i=0; i<iterations; i++)
		{
			for(Node child : this.children)
			{
				obstacle.append(child.createObstacle());
			}
		}
		return obstacle.toString();
	}

	@Override
	public String createSolution() {
		StringBuilder solution = new StringBuilder();
		solution.append("for(int i=0; i<"+iterations+"; i++) {\n");
		
		for(Node child : this.children)
		{
			solution.append(child.createSolution());
		}
		
		solution.append("}\n");
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
		case 1:
			ChangeIterations();
			break;
		default:			
			break;
		}
	}	
	
	public Node ChangeIterations()
	{
		this.iterations = Lottery.getRandomNumber(minIterations, maxIterations);
		return this;
	}
}

class SkeletonLoop extends SkeletonNode{

	public SkeletonLoop() {
		super("Loop", 40);
	}

	@Override
	public Node create(Node parent, String name) {
		return new Loop(parent, name);
	}
	
}
