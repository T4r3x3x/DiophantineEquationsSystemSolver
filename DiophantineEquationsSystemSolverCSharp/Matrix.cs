using System.Collections;

namespace DiophantineEquationsSystemSolverCSharp
{
    public class Matrix(int[][] values) : IEnumerable<int[]>
    {
        public int[][] Values = values;

        public int this[int row, int column]
        {
            get => Values[row][column];
            set => Values[row][column] = value;
        }

        public int[] this[int row] => Values[row];

        public int RowCount => Values.Length;

        public int ColumnCount => Values[0].Length;

        public IEnumerator<int[]> GetEnumerator() => (IEnumerator<int[]>)Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}