package june;

public abstract class Direction implements Vector3
{
    public Direction(){

    }

    public static Direction up(){
        return new LazyDirection(0,1,0);
    }

    public static Direction down(){
        return new LazyDirection(0,-1,0);
    }

    public static Direction west(){
        return new LazyDirection(-1,0,0);
    }
    
    public static Direction east(){
        return new LazyDirection(1,0,0);
    }
    
    public static Direction north(){
        return new LazyDirection(0,0,1);
    }
    
    public static Direction south(){
        return new LazyDirection(0,0,-1);
    }
    
    public static Direction right() {
        String vector3_string = "objects['Me'].transform.right";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }
    
    public static Direction left() {
        String vector3_string = "objects['Me'].transform.right * -1";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction forward() {
        String vector3_string = "objects['Me'].transform.FindChild('Main Camera').transform.forward";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction backward() {
        String vector3_string = "objects['Me'].transform.forward * -1";
        Direction dir = new LazyDirection(vector3_string);
            
        return dir;
    }

    public static Direction none() {
        return new LazyDirection(0,0,0);
    }
    
    
    public static Direction between(Location source, Location target) {
        String vector3_string = "("+target.toString()+"-"+source.toString()+").normalized";
        Direction dir = new LazyDirection(vector3_string);
        return dir;
    }


    public static Direction between(Enchanted source, Enchanted target)
    {
       String vector3_string = "("+target.getLocation()+"-"+source.getLocation()+").normalized";
            
       Direction dir = new LazyDirection(vector3_string);

       return dir;
    }

    public abstract Vector3 add(Vector3 v);
    public abstract Vector3 times(double d);
    public abstract void freeze();
}
