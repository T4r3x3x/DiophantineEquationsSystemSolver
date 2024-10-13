namespace DiophantineEquationsSystemSolverCSharp
{
    public static class StringExtensions
    {
        public static IEnumerable<int> ToIntEnum(this string line, char separator = ' ') =>
            line.Split(separator).Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x));


        public static double[] ToDoubles(this string line, char separator = ' ') =>
            line.Split(separator).Where(x => !string.IsNullOrEmpty(x)).Select(Convert.ToDouble).ToArray();
    }
}