using Visitor;

var circle = new Circle(4);
var rectangle = new Rectangle(5, 6);
var triangle = new Triangle(2, 3, 5);

var consoleWriter = new ConsoleWriter();

//Direct invoke
circle.Accept(consoleWriter);

//Through interface
var shapes = new List<IVisitable>() { circle, rectangle, triangle };
foreach (var shape in shapes)
{
    //Circle { Radius = 4 }
    //Rectangle { Width = 5, Height = 6 }
    //I am not implemented!
    shape.Accept(consoleWriter);
}

var areaCalculator = new AreaCalculator();

// Explicitly type is required for a visitor with some targets and returned result. Maybe it can be fixed.
var circleArea = circle.Accept<Circle, double>(areaCalculator);
var rectangleArea = rectangle.Accept<Rectangle, double>(areaCalculator);
//var triangleArea = triangle.Accept<Triangle, double>(areaCalculator); Compile error because it isn't implemented

Console.WriteLine(circleArea); //50.265...
Console.WriteLine(rectangleArea); //30
