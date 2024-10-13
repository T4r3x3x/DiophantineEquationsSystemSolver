namespace DiophantineEquationsSystemSolverCSharp
{
    public static class MatrixExtensions
    {

        public static (int index, bool isFound) Min(this Matrix matrix, int rowIndex)
        {
            var row = matrix[rowIndex];
            (var min, var index) = (double.MaxValue, 0);

            for (var i = rowIndex; i < row.Length - 1; i++)
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

            for (var i = 0; i < matrix.ColumnCount - 1; i++)
                expanded[matrix.RowCount + i] = GenerateAdditionalRow(i, matrix.ColumnCount);

            matrix.Values = expanded;
        }

        private static double[][] InitializeExpanded(Matrix matrix)
        {
            var expanded = new double[matrix.RowCount + matrix.ColumnCount - 1][];
            for (var i = 0; i < matrix.RowCount; i++)
                expanded[i] = matrix[i];

            return expanded;
        }

        private static double[] GenerateAdditionalRow(int rowNumber, int length)
        {
            var additionalRow = new double[length];
            for (var j = 0; j < length; j++)
                additionalRow[j] = j switch
                {
                    _ when j == rowNumber => 1,
                    _ => 0
                };

            return additionalRow;
        }
    }
}