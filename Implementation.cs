namespace Visitor;

public record Circle(double Radius) : IVisitable;

public record Rectangle(double Width, double Height) : IVisitable;

public record Triangle(double A, double B, double C) : IVisitable;

public class ConsoleWriter : ITargetVisitor<Circle>, ITargetVisitor<Rectangle>, ITargetVisitor<IVisitable>
{
    public void Visit(Circle item)
    {
        Console.WriteLine(item);
    }

    public void Visit(Rectangle item)
    {
        Console.WriteLine(item);
    }

    public void Visit(IVisitable item)
    {
        switch (item)
        {
            case Circle circle:
                Visit(circle);
                break;
            case Rectangle rectangle:
                Visit(rectangle);
                break;
            default:
                Console.WriteLine("I am not implemented!");
                break;
        }
    }
}

public class AreaCalculator : ITargetVisitor<Circle, double>, ITargetVisitor<Rectangle, double>
{
    public double Visit(Circle item) => Math.PI * item.Radius * item.Radius;

    public double Visit(Rectangle item) => item.Height * item.Width;
}