import java.util.ArrayList;
import java.util.Arrays;


public class Conditional extends Branch {
		
	// Let's try this for now
	private static ArrayList<String> conditions = new ArrayList<String>(
				Arrays.asList(
								"leftDoor.isOpen()",
								"rightDoor.isOpen()"
							 )
			);
	
	private String condition = Conditional.conditions.get(0);
	
	public Conditional(Node parent, String name) {
		super(parent, name);
		TreeStats.setTickets(this, TreeStats.conditionalLiveListProbability, TreeStats.conditionalActionProbability);

		this.AddChild(new Case<Boolean>(this, "TrueCase", true));
		this.AddChild(new Case<Boolean>(this, "FalseCase", false));
	}
	
	@Override
	public String createObstacle() {
		StringBuilder obstacle = new StringBuilder();
		obstacle.append("[Begin "+this.name+"]\n");
		
		for(Node child : this.children)
		{
			obstacle.append(child.createObstacle());
		}
			
		obstacle.append("[End "+this.name+"]\n");
		return obstacle.toString();
	}

	@Override
	public String createSolution() {
		StringBuilder solution = new StringBuilder();
		
		solution.append("if ("+this.condition+") {\n");
		solution.append(this.children.get(0).createSolution());
		solution.append("}\n");
		
		solution.append("else {\n");
		solution.append(this.children.get(1).createSolution());
		solution.append("}\n");
		
		return solution.toString();
	}

	@Override
	public void PerformRandomAction() {
		Action winningAction = Lottery.getWinner(this.getActionTickets(), this.getTotalActionTickets());
		
		switch(winningAction.getActionId())
		{
		case 0:
			ChangeCondition();
			break;
		default:			
			break;
		}
	}	
	
	public void ChangeCondition()
	{
		this.condition = Lottery.getRandomElement(Conditional.conditions);
	}
	
	@Override
	public boolean HasLeafAsDescendantCanBeSet() {
		// Mark ourselves as having leaf as descendant IF all our children have it
		for (Node child : this.children)
		{
			if(!child.hasLeafAsDescendant()) return false;
		}
		
		return true;
	}
}

class SkeletonConditional extends SkeletonNode{

	public SkeletonConditional() {
		super("Conditional", 40);
	}

	@Override
	public Node create(Node parent, String name) {
		return new Conditional(parent, name);
	}
	
}
