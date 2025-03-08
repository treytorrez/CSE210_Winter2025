public class Square : Shape
{
    private double _side;

    public Square(double side)
    { _side = side; }
    override public double GetArea()
    {
        return _side * _side;
    }
}