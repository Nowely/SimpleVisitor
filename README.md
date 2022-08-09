# Simple visitor pattern

The realization of visitor design pattern with two interfaces and one extension method.

## Realization

### No return value

The `IVisitable` interface for mark classes that can be to visit:

```csharp
public interface IVisitable {}
```

The `ITargetVisitor` for build a visitor where `T` is a class with inherited `IVisitable`:

```csharp
public interface ITargetVisitor<in T>
{
    void Visit(T item);
}
```

The extension `Accept` for `IVisitable` objects:

```csharp
public static class VisitorExtension
{
    public static void Accept<T>(this T visitable, ITargetVisitor<T> visitor)
        where T : IVisitable => visitor.Visit(visitable);
}
```

### Generic returned value

The `IVisitable` without changes.

```csharp
public interface ITargetVisitor<in T, out T1> where T : IVisitable
{
    T1 Visit(T item);
}
```

```csharp
public static class VisitorExtension
{
    public static T1 Accept<T, T1>(this T visitable, ITargetVisitor<T, T1> visitor)
        where T : IVisitable => visitor.Visit(visitable);
}
```

## Example

For instances:

```csharp
var circle = new Circle(4);
var rectangle = new Rectangle(5, 6);
var triangle = new Triangle(2, 3, 5);
```

No return value usage:

```csharp
var consoleWriter = new ConsoleWriter();

//Direct invoke
circle.Accept(consoleWriter); //Output: Circle { Radius = 4 }

//Invoke via interface
var shapes = new List<IVisitable>() { circle, rectangle, triangle };
foreach (var shape in shapes)
{
    shape.Accept(consoleWriter);
}

//Output: 
//  Circle { Radius = 4 }
//  Rectangle { Width = 5, Height = 6 }
//  I am not implemented!
```

With returned value:

```csharp
var areaCalculator = new AreaCalculator();

// Explicitly type is required for a visitor with some targets and returned result. Maybe it can be fixed.
var circleArea = circle.Accept<Circle, double>(areaCalculator);
var rectangleArea = rectangle.Accept<Rectangle, double>(areaCalculator);
//var triangleArea = triangle.Accept<Triangle, double>(areaCalculator); Compile error because it isn't implemented

Console.WriteLine(circleArea); //Output: 50.265...
Console.WriteLine(rectangleArea); //Output: 30
```

## Implementation

Records:

```csharp
public record Circle(double Radius) : IVisitable;

public record Rectangle(double Width, double Height) : IVisitable;

public record Triangle(double A, double B, double C) : IVisitable;
```

The `ConsoleWriter` visitor with implemented supported targets:

```csharp
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

    //For work with interface and no implemented classes
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
 ```

The `AreaCalculator` visitor with returned value:

```csharp
public class AreaCalculator : ITargetVisitor<Circle, double>, ITargetVisitor<Rectangle, double>
{
    public double Visit(Circle item) => Math.PI * item.Radius * item.Radius;

    public double Visit(Rectangle item) => item.Height * item.Width;
}
```