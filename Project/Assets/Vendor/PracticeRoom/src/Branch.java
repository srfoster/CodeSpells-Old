
public abstract class Branch extends Node{

	public Branch(Node parent, String name) {
		super(parent, name);
	}
	
	public void AddChild()
	{
		SkeletonNode winningChildType = Lottery.getWinner(nodeTypes.nodeTypes, nodeTypes.getTotalNodeTypeTickets());
		
		this.AddChild(winningChildType.create(this, winningChildType.getBaseName()));
	}
	
	public void AddChild(Node newChild)
	{
		int id = TreeBuilder.getLiveListSize();
		newChild.name = newChild.name+id;
		
		TreeBuilder.addToLiveList(newChild);
		this.getChildren().add(newChild);
	}
}
