import java.io.*;

public class PracticeRoom {
	private static String outputFile;
	
	public static void main(String [] args)
	{
		//I made a change homie G dawg
		outputFile = args[0];
		
		TreeBuilder.setUpNodes();
		Node currNode;
		while(!TreeBuilder.complete())
		{
			currNode = TreeBuilder.getRandomNode();
			currNode.PerformRandomAction();
		}
		
		// The following code breaks conditionals...because we prune away the true/false case if they've got no children
		TreeBuilder.cleanup(TreeBuilder.getRoot());
		
		String obstacle = TreeBuilder.getRoot().createObstacle();
		String solution  = TreeBuilder.getRoot().createSolution();

		// Print to standard output (for debugging)
		System.out.println("Obstacle: \n" + obstacle);
		System.out.println();
		System.out.println("Solution: \n" + solution);
		
		// Print to file (for Unity)
		try {
			FileWriter fstream = new FileWriter(outputFile);
			BufferedWriter out = new BufferedWriter(fstream);
			out.write(obstacle);
			out.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
