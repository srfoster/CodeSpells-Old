package june;

public abstract class Location implements Vector3
{
    public Location(){

    }

    public Location adjust(Vector3 dir)
    {
      return adjust(dir, 1.0);
    }
    
    public Location adjust(Vector3 dir, double scale) {
        //Vector3 dir is a unit vector
        Direction new_dir = (Direction) dir.times(scale);

        Location new_loc  = (Location) add(new_dir);

        return (Location) new_loc;
    }

    public abstract Vector3 add(Vector3 v);
    public abstract Vector3 times(double d);
    public abstract double distanceBetween(Location loc);
    public abstract void freeze();
}
