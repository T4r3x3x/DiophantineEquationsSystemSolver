using System.Collections;

namespace DiophantineEquationsSystemSolverCSharp;

public class EquationsSystem : IEnumerable<int[]>
{
    private readonly List<int[]> _rows;

    public EquationsSystem(int[][] values)
    {
        _rows = [..values];
        OriginRowsCount = _rows.Count;
        OriginColumnsCount = _rows[0].Length;
        Expand();
    }

    private int VariablesCount => _rows[0].Length - 1;

    public int[] this[int row] => _rows[row];

    public int OriginRowsCount { get; }

    public int OriginColumnsCount { get; }

    public int RowCount => _rows.Count;

    public int ColumnCount => _rows[0].Length;

    public IEnumerator<int[]> GetEnumerator() => _rows.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Expand()
    {
        var additionalRowsCount = VariablesCount;
        for (var i = 0; i < additionalRowsCount; i++)
            _rows.Add(GenerateAdditionalRow(i, ColumnCount));
    }

    private static int[] GenerateAdditionalRow(int additionalRowNumber, int length)
    {
        var additionalRow = new int[length];
        for (var j = 0; j < length; j++)
            additionalRow[j] = j switch
            {
                _ when j == additionalRowNumber => 1,
                _ => 0
            };

        return additionalRow;
    }
}