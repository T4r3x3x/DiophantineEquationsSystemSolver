namespace DiophantineEquationsSystemSolverCSharp
{
    public static class MatrixExtensions
    {

        public static (int index, bool isFound) Min(this Matrix matrix, int rowIndex, int count)
        {
            var row = matrix[rowIndex];
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
        public static void Expand(this Matrix matrix)
        {
            var expanded = InitializeExpanded(matrix);

            for (var i = matrix.RowCount; i < expanded.Length; i++)
                expanded[i] = GenerateAdditionalRow(i - matrix.RowCount, matrix.ColumnCount);

            matrix.Values = expanded;
        }

        private static int[][] InitializeExpanded(Matrix matrix)
        {
            var expanded = new int[matrix.RowCount + matrix.ColumnCount - 1][];
            for (var i = 0; i < matrix.RowCount; i++)
                expanded[i] = matrix[i];

            return expanded;
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
}