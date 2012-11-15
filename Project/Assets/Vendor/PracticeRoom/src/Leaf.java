
public abstract class Leaf extends Node{

	public Leaf(Node parent, String name) {
		super(parent, name);
		this.setHasLeafAsDescendant(true);
	}
}
