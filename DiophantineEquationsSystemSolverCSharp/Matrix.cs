using System.Collections;

namespace DiophantineEquationsSystemSolverCSharp
{
    public class Matrix(double[][] values) : IEnumerable<double[]>
    {
        public double[][] Values = values;

        public double this[int row, int column]
        {
            get => Values[row][column];
            set => Values[row][column] = value;
        }

        public double[] this[int row] => Values[row];
        public int RowCount => Values.Length;

        public int ColumnCount => Values[0].Length;
        public IEnumerator<double[]> GetEnumerator() => (IEnumerator<double[]>)Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}