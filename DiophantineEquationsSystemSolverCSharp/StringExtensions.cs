namespace DiophantineEquationsSystemSolverCSharp
{
    public static class StringExtensions
    {
        public static IEnumerable<int> ToIntEnum(this string line, char separator = ' ') =>
            line.Split(separator).Select(x => Convert.ToInt32(x));


        public static double[] ToDoubles(this string line, char separator = ' ') =>
            line.Split(separator).Select(Convert.ToDouble).ToArray();
    }
}