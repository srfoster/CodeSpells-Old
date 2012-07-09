package june;

public class Direction
{
    public static final int NONE = 0;
    public static final int WEST = 1;
    public static final int EAST = 2;
    public static final int SOUTH = 3;
    public static final int NORTH = 4;
    public static final int LEFT = 5;
    public static final int RIGHT = 6;
    public static final int UP = 7;
    public static final int DOWN = 8;
    public static final int FORWARD = 9;
    public static final int BACKWARD = 10;
    
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

    public static int none() {
        return 0;
    }

    public static Location toLocation(int dir)
    {
        Location loc = new Location(0,0,0);

        if(dir == Direction.NORTH) loc.x += 1.0;//north
        if(dir == Direction.SOUTH) loc.x -= 1.0;//south
        if(dir == Direction.UP) loc.y += 1.0;//west
        if(dir == Direction.DOWN) loc.y -= 1.0;//west
        if(dir == Direction.EAST) loc.z -= 1.0;//east
        if(dir == Direction.WEST) loc.z += 1.0;//west

        if(dir == Direction.LEFT)
        {
            String vector3_string = "objects['Me'].transform.right * -1";
            
            loc = new LazyLocation(vector3_string);
        }
        if(dir == Direction.RIGHT) {
            String vector3_string = "objects['Me'].transform.right";
            
            loc = new LazyLocation(vector3_string);
        }
        if(dir == Direction.FORWARD) 
        {
            String vector3_string = "objects['Me'].transform.forward";
            
            loc = new LazyLocation(vector3_string);
        }
        if(dir == Direction.BACKWARD) {
            String vector3_string = "objects['Me'].transform.forward * - 1";
            
            loc = new LazyLocation(vector3_string);
        }

        return loc;
    }
    
}
