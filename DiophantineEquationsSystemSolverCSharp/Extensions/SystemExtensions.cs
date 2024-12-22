namespace DiophantineEquationsSystemSolverCSharp.Extensions;

public static class SystemExtensions
{
    public static (int index, bool isFound) Min(this EquationsSystem system, int rowIndex, int count)
    {
        var row = system[rowIndex];
        (var min, var index) = (double.MaxValue, 0);

        for (var i = count; i < row.Length - 1; i++)
            if (Math.Abs(row[i]) < min && row[i] != 0)
                (min, index) = (row[i], i);

        return min switch
        {
            double.MaxValue => (0, false),
            _ => (index, true)
        };
    }

    public static bool IsRowTransformed(this EquationsSystem system, int rowNumber, int skipCount) =>
        system[rowNumber].Skip(skipCount).SkipLast(1).Count(val => val != 0) <= 1 && system[rowNumber][^1] == 0;

    public static bool IsColumnZeroed(this EquationsSystem system, int columnNumber)
    {
        for (int i = 0; i < system.OriginRowsCount; i++)
            if (system[i][columnNumber] != 0)
                return false;
        return true;
    }

    public static void SwapColumns(this EquationsSystem system, int rowNumber, int minValueIndex)
    {
        for (var i = 0; i < system.RowCount; i++)
            (system[i][rowNumber], system[i][minValueIndex]) = (system[i][minValueIndex], system[i][rowNumber]);
    }
}