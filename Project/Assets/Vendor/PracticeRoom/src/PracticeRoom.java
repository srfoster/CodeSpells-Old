import java.io.*;

public class PracticeRoom {
	private static String outputFile;
	
	public static void main(String [] args)
	{
		outputFile = args[0];
		
		TreeBuilder.setUpNodes();
		Node currNode;
		while(!TreeBuilder.complete())
		{
			currNode = TreeBuilder.getRandomNode();
			currNode.PerformRandomAction();
		}
	
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
