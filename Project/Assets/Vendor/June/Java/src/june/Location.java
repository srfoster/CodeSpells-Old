package june;

public abstract class Location implements Vector3
{
    public Location(){

    }

    public void adjust(Vector3 dir)
    {
      adjust(dir, 1.0);
    }
    
    public void adjust(Vector3 dir, double scale) {
        //Vector3 dir is a unit vector
        dir.times(scale);

        add(dir);
    }

    public abstract void add(Vector3 v);
    public abstract void times(double d);
    public abstract double distanceBetween(Location loc);
    public abstract void freeze();
}
