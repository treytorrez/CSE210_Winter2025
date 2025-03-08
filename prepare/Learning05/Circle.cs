public class Circle : Shape
{
    private double _radius;

    public Circle(double radius)
    { _radius = radius; }

    override public double GetArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }

}