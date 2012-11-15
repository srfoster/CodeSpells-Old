
public class Wall extends Leaf {

	public Wall(Node parent, String name) {
		super(parent, name);
		TreeStats.setTickets(this, TreeStats.wallLiveListProbability, TreeStats.wallActionProbability);
	}
	
	@Override
	public String createObstacle() {
		return "WALL"+System.getProperty("line.separator");
	}

	@Override
	public String createSolution() {
		return "jumpWall();\n";
	}

	@Override
	public void PerformRandomAction() {
		
	}
}

class SkeletonWall extends SkeletonNode{

	public SkeletonWall() {
		super("Wall", 10);
	}

	@Override
	public Node create(Node parent, String name) {
		return new Wall(parent, name);
	}
	
}
