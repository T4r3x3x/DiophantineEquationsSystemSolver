using DiophantineEquationsSystemSolverCSharp;

int n = 1, m = 2;
double[] c = [-2, 0];
var matrix = new Matrix([[3, 4, 0,-8],[7, 0, 5,-6]]);
var originRowsCount = matrix.RowCount;
var freeMembersCount = matrix.ColumnCount - matrix.RowCount - 1;
matrix.Expand();


try 
{
    for (var rowIndex = 0; rowIndex < originRowsCount; rowIndex++)
    {
        var nonZeroNumberIndex = ZeroRow(matrix, rowIndex);
        SwapColumns(matrix, rowIndex, nonZeroNumberIndex);
    }
    WriteSolution(matrix, originRowsCount, freeMembersCount);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

void WriteSolution(Matrix matrix, int originRowsCount, int freeMembersCount)
{
    for (int i = originRowsCount; i < matrix.RowCount; i++)
    {
        for (int j = 1; j <= freeMembersCount + 1; j++)
            Console.Write(matrix[i][^j] + " ");
        Console.WriteLine();
    }
}

void SwapColumns(Matrix matrix, int rowNumber, int minValueIndex)
{
    for (var i = 0; i < matrix.RowCount; i++)
        (matrix[i][rowNumber], matrix[i][minValueIndex]) = (matrix[i][minValueIndex], matrix[i][rowNumber]);
}


int ZeroRow(Matrix matrix, int rowNumber)
{
    var index = 0;
    while (!IsRowZeroed(matrix, rowNumber))
    {
        var res = matrix.Min(rowNumber);
        index = res.index;
        if (!res.isFound)
            throw new("Min value not found");
        Subtract(matrix, rowNumber, res.index);
    }
    return index;
}

bool IsRowZeroed(Matrix matrix, int rowNumber) =>
    matrix[rowNumber].Count(val => val != 0) == rowNumber + 1;

void Subtract(Matrix matrix, int rowNumber, int minValueIndex)
{
    var minuendColumnIndex = GetMinuend(matrix, rowNumber, minValueIndex);
    SubtractColumns(matrix, minuendColumnIndex, minValueIndex, rowNumber);
}

int GetMinuend(Matrix matrix, int rowNumber, int minValueIndex)
{
    for (var i = rowNumber; i < matrix[rowNumber].Length; i++)
        if (matrix[rowNumber][i] != 0 && i != minValueIndex)
            return i;

    throw new("Can't find minuend");
}

void SubtractColumns(Matrix matrix, int minuendColumnIndex, int subtrahendColumnIndex, int rowNumber)
{
    var coef = (int)(matrix[rowNumber][minuendColumnIndex] / matrix[rowNumber][subtrahendColumnIndex]);
    if (coef == 0)
        throw new("Coef is zero");
    for (var i = rowNumber; i < matrix.RowCount; i++)
        matrix[i][minuendColumnIndex] -= matrix[i][subtrahendColumnIndex] * coef;
}