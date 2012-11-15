import java.util.ArrayList;
import java.util.Iterator;

public class TreeBuilder {	
	private static ArrayList<Node> liveList;
	private static int totalNodeTickets = 0;
	
	private static Node root;
	
	public static int getLiveListSize()
	{
		return liveList.size();
	}
	
	public static void addToLiveList(Node node)
	{
		liveList.add(node);
		totalNodeTickets += node.getLiveListTickets();
	}

	public static Node getRoot() {
		return root;
	}

	public static void setUpNodes()
	{
		liveList = new ArrayList<Node>();
		root = new Root();
		addToLiveList(root);
	}
	
	public static Node getRandomNode()
	{
		return Lottery.getWinner(liveList, totalNodeTickets);
	}
	
	public static boolean complete()
	{
		return getLiveListSize() >= 12;
	}
	
	public static void cleanup(Node currNode)
	{
		// Cleanup each of my children (if I have any)
		if (currNode.getChildren() != null)
		{
			Iterator<Node> childIterator = currNode.getChildren().iterator();
			
			Node child;
			while(childIterator.hasNext())
			{
				child = childIterator.next();
				
				// If there's no leaf at the end of this path, prune it out
				if (!child.hasLeafAsDescendant())
				{
					childIterator.remove();
				}
				else
				{
					// Otherwise, recursively check each child we didn't remove
					TreeBuilder.cleanup(child);
				}
			}
		}
	}
}
