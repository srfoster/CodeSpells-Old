package june;

public class Direction
{
    private static final int WEST = 1;
    private static final int EAST = 2;
    private static final int SOUTH = 3;
    private static final int NORTH = 4;
    private static final int LEFT = 5;
    private static final int RIGHT = 6;
    
    private static int direct;
    
    
    public Direction(int dir) {
        direct = dir;
    }
    
    public int getDirection() {
        return direct;
    }
    
    public static int west(){
        return 1;
    }
    
    public static int east(){
        return 2;
    }
    
    public static int north(){
        return 4;
    }
    
    public static int south(){
        return 3;
    }
    
    public static int right() {
        return 6;
    }
    
    public static int left() {
        return 5;
    }
    
}