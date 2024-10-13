using DiophantineEquationsSystemSolverCSharp;

(var originRowsCount, var originColumnsCount) = Console.ReadLine()!.ToIntEnum(' ');

var nums = new double[originRowsCount][];
for (int i = 0; i < originRowsCount; i++)
    nums[i] = Console.ReadLine()!.ToDoubles();

var matrix = new Matrix(nums);
matrix.Expand();

try
{
    for (var rowIndex = 0; rowIndex < originRowsCount; rowIndex++)
    {
        var nonZeroNumberIndex = ZeroRow(matrix, rowIndex);
        if (nonZeroNumberIndex != 0)
            SwapColumns(matrix, rowIndex, nonZeroNumberIndex);
    }
    WriteSolution(matrix, originRowsCount, GetFreeVariablesCount(matrix, originRowsCount));
}
catch (Exception e)
{
#if DEBUG
    Console.WriteLine(e.Message);
#endif
    Console.WriteLine("NO SOLUTIONS");
}

int GetFreeVariablesCount(Matrix matrix, int originRowsCount)
{
    var count = 0;

    for (int i = 0; i < matrix.ColumnCount - 1; i++)
        if (IsColumnZeroed(matrix, originRowsCount, i))
            count++;

    return count;
}

bool IsColumnZeroed(Matrix matrix, int originRowsCount, int columnNumber)
{
    for (int i = 0; i < originRowsCount; i++)
        if (matrix[i][columnNumber] != 0)
            return false;
    return true;
}

void WriteSolution(Matrix matrix, int originRowsCount, int freeVariablesCount)
{
    Console.WriteLine(freeVariablesCount);
    for (int i = originRowsCount; i < matrix.RowCount; i++)
    {
        for (int j = matrix.ColumnCount - freeVariablesCount - 1; j < matrix.ColumnCount; j++)
            Console.Write(matrix[i][j] + " ");
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
    matrix[rowNumber].Skip(rowNumber + 1).All(val => val == 0);

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