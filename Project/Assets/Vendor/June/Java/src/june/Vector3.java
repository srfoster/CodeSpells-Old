package june;

public interface Vector3{
  public String getXString();
  public String getYString();
  public String getZString();

  public Vector3 add(Vector3 other);
  public Vector3 times(double s);
}
