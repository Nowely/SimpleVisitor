namespace Visitor;

public interface IVisitable
{
}
public interface ITargetVisitor<in T> //TODO where T : IVisitable ?
{
    void Visit(T item);
}

//With returned value - optional
public interface ITargetVisitor<in T, out T1>
{
    T1 Visit(T item);
}

public static class VisitorExtension
{
    public static void Accept<T>(this T visitable, ITargetVisitor<T> visitor)
        where T : IVisitable => visitor.Visit(visitable);

    //Extension for visitor with returned value 
    public static T1 Accept<T, T1>(this T visitable, ITargetVisitor<T, T1> visitor)
        where T : IVisitable => visitor.Visit(visitable);
}
