namespace DiophantineEquationsSystemSolverCSharp.Extensions;

public static class StringExtensions
{
    public static int[] ToInts(this string line, char separator = ' ') =>
        line.Split(separator)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => Convert.ToInt32(x))
            .ToArray();

}