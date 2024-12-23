namespace DiophantineEquationsSystemSolverCSharp.Extensions;

public static class EnumerableExtensions
{
    public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T first, out T second)
    {
        var array = enumerable as T[] ?? enumerable.ToArray();
        first = (array.Length != 0 ? array[0] : default) ?? throw new InvalidOperationException();
        second = (array.Length > 1 ? array[1] : default) ?? throw new InvalidOperationException();
    }


}