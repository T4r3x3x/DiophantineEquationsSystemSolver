namespace DiophantineEquationsSystemSolverCSharp.Extensions;

public static class IntExtensions
{
    public static int GetGreatestCommonDivider(int[] numbers) => numbers.Aggregate(GetGreatestCommonDivider);

    public static int GetGreatestCommonDivider(int a, int b)
    {
        while (true)
        {
            if (b == 0)
                return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }
}