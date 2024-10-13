using DiophantineEquationsSystemSolverCSharp;

int n = 1, m = 2;
double[] c = [-2, 0];
var matrix = new Matrix([[3, 4, 0, -8], [7, 0, 5, -6]]);
var originRowsCount = matrix.RowCount;

var expanded = matrix.Expand();


try
{
    for (var rowIndex = 0; rowIndex < originRowsCount; rowIndex++)
    {
        var nonZeroNumberIndex = ZeroRow(matrix, rowIndex);
        SwapColumns(matrix, rowIndex, nonZeroNumberIndex);
    }
    Console.WriteLine("A");
}
catch
{
    Console.WriteLine("TI CHE DEBIL?");
}

void SwapColumns(Matrix matrix, int rowNumber, int minValueIndex)
{
    for (var i = 0; i < matrix.RowCount; i++)
        (matrix[i][rowNumber], matrix[i][minValueIndex]) = (matrix[i][minValueIndex], matrix[i][rowNumber]);
}


int ZeroRow(Matrix matrix, int rowNumber)
{
    var index = 0;
    while (!isRowZeroed(matrix, rowNumber))
    {
        var res = matrix.Min(rowNumber);
        index = res.index;
        if (!res.isFound)
            throw new("KURWA");
        Subtract(matrix, rowNumber, res.index);
    }
    return index;
}

bool isRowZeroed(Matrix matrix, int rowNumber)
{
    for (var i = rowNumber + 1; i < matrix.ColumnCount; i++)
        if (matrix[rowNumber][i] != 0)
            return false;

    return true;
}

void Subtract(Matrix matrix, int rowNumber, int minValueIndex)
{
    var minuendColumnIndex = GetMinuend(matrix, rowNumber, minValueIndex);
    SubtractColumns(matrix, minuendColumnIndex, minValueIndex, rowNumber);
}

int GetMinuend(Matrix matrix, int rowNumber, int minValueIndex)
{
    for (var i = 0; i < matrix[rowNumber].Length; i++)
        if (matrix[rowNumber][i] != 0 && i != minValueIndex)
            return i;

    throw new("kurwa");
}

void SubtractColumns(Matrix matrix, int minuendColumnIndex, int subtrahendColumnIndex, int rowNumber)
{
    var coef = (int)(matrix[rowNumber][minuendColumnIndex] / matrix[rowNumber][subtrahendColumnIndex]);
    if (coef == 0)
        throw new("KURWA");
    for (var i = rowNumber; i < matrix.RowCount; i++)
        matrix[i][minuendColumnIndex] -= matrix[i][subtrahendColumnIndex] * coef;
}