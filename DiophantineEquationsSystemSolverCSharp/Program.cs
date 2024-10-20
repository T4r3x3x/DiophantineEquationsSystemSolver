using DiophantineEquationsSystemSolverCSharp;

(var originRowsCount, var originColumnsCount) = Console.ReadLine()!.ToInts();

var nums = new int[originRowsCount][];
for (int i = 0; i < originRowsCount; i++)
{
    nums[i] = Console.ReadLine()!.ToInts();
    nums[i][^1] *= -1;
}
var matrix = new Matrix(nums);
matrix.Expand();
Console.WriteLine();
try
{
    for (var rowIndex = 0; rowIndex < originRowsCount; rowIndex++)
    {
        var nonZeroNumberIndex = ZeroRow(matrix, rowIndex);
        if (NeedToSwap(matrix, rowIndex, nonZeroNumberIndex))
            SwapColumns(matrix, rowIndex, nonZeroNumberIndex);
    }
    if (!IsColumnZeroed(matrix, originRowsCount, originColumnsCount))
        throw new ArgumentException("Last column contains non zero elements");
    SimplifySolution(matrix);
    WriteSolution(matrix, originRowsCount, GetFreeVariablesCount(matrix, originRowsCount));

}
catch (Exception e)
{
#if DEBUG
    Console.WriteLine(e.Message);
#endif
    Console.WriteLine("NO SOLUTIONS");
}

void SimplifySolution(Matrix matrix)
{
    for (int i = originRowsCount; i < matrix.RowCount; i++)
    {
        var a = matrix[i];
        //var gcd = GCDMat();

    }
}


bool NeedToSwap(Matrix matrix, int rowNumber, int nonZeroNumberIndex)
{
    var skipCount = rowNumber < matrix.ColumnCount - 1 ? rowNumber + 1 : matrix.ColumnCount - 1;
    return matrix[rowNumber].Skip(skipCount).Any(val => val != 0);
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
        Console.Write(matrix[i][^1] + " ");
        for (int j = matrix.ColumnCount - freeVariablesCount - 1; j < matrix.ColumnCount-1; j++)
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
    var count = rowNumber < matrix.ColumnCount - 1 ? rowNumber : matrix.ColumnCount - 2;
    while (!IsRowZeroed(matrix, rowNumber, count))
    {
        var res = matrix.Min(rowNumber, count);
        index = res.index;
        if (!res.isFound)
            throw new($"Min value not found at row: {rowNumber} |" + string.Join(' ', matrix[rowNumber]));
        Subtract(matrix, rowNumber, res.index);
    }
    return index;
}

bool IsRowZeroed(Matrix matrix, int rowNumber, int count) =>
    matrix[rowNumber].Skip(count).SkipLast(1).Count(val => val != 0) <= 1 && matrix[rowNumber][^1] == 0;

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
    for (var i = 0; i < matrix.RowCount; i++)
        matrix[i][minuendColumnIndex] -= matrix[i][subtrahendColumnIndex] * coef;
}

int GCDMat(int[] numbers)
{
    return numbers.Aggregate(GCD);
}

int GCD(int a, int b)
{
    return b == 0 ? a : GCD(b, a % b);
}