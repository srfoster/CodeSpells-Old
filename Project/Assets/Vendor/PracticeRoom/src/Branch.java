
public abstract class Branch extends Node{

	public Branch(Node parent, String name) {
		super(parent, name);
	}
	
	public Node AddChild()
	{
		SkeletonNode winningChildType = Lottery.getWinner(nodeTypes.nodeTypes, nodeTypes.getTotalNodeTypeTickets());
		
		int id = TreeBuilder.getLiveListSize();
		Node child = winningChildType.create(this, winningChildType.getBaseName()+id);
		
		TreeBuilder.addToLiveList(child);
		this.getChildren().add(child);
		return child;
	}
}
