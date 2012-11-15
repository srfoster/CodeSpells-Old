import java.util.ArrayList;


public class NodeTypes {
	public ArrayList<SkeletonNode> nodeTypes;
	private int totalNodeTypeTickets = 0;
	
	public NodeTypes()
	{
		nodeTypes = new ArrayList<SkeletonNode>();
		nodeTypes.add(new SkeletonRiver());
		nodeTypes.add(new SkeletonWall());
		nodeTypes.add(new SkeletonLoop());
	}
	
	public void changeProbability(int river, int wall, int loop)
	{
		nodeTypes.get(0).setCreatingMeTickets(river);
		nodeTypes.get(1).setCreatingMeTickets(wall);
		nodeTypes.get(2).setCreatingMeTickets(loop);
		setTotalNodeTypeTickets(river + wall + loop);
	}
	
	public int size()
	{
		return nodeTypes.size();
	}
	
	public SkeletonNode get(int index)
	{
		return nodeTypes.get(index);
	}

	public int getTotalNodeTypeTickets() {
		return totalNodeTypeTickets;
	}

	public void setTotalNodeTypeTickets(int totalNodeTypeTickets) {
		this.totalNodeTypeTickets = totalNodeTypeTickets;
	}
}
