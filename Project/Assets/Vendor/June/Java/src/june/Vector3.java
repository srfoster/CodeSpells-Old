package june;

public interface Vector3{
  public String getXString();
  public String getYString();
  public String getZString();

  public void add(Vector3 other);
  public void times(double s);
}
