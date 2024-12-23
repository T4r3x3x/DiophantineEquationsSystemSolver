using DiophantineEquationsSystemSolverCSharp.Extensions;

namespace DiophantineEquationsSystemSolverCSharp.Solver;

public class Solver(EquationsSystem system)
{
    public Solution Solve()
    {
        try
        {
            ProduceSolving();
        }
        catch (SolutionException e)
        {
            return Solution.NoSolution(e.Message);
        }
        catch (Exception e)
        {
            return Solution.FromException(e);
        }
        var freeVariablesCount = GetFreeVariablesCount();
        var solution = ExtractSolution(freeVariablesCount);
        return Solution.CreateSolved(freeVariablesCount, solution);
    }

    private int[][] ExtractSolution(int freeVariablesCount)
    {
        var solution = new int[system.RowCount - system.OriginRowsCount][];
        for (int i = system.OriginRowsCount; i < system.RowCount; i++)
        {
            var offsetI = i - system.OriginRowsCount;
            solution[offsetI] = new int[1 + freeVariablesCount];
            solution[offsetI][0] = system[i][^1];
            for (int j = system.ColumnCount - freeVariablesCount - 1; j < system.ColumnCount - 1; j++)
            {
                var offsetj = j - system.ColumnCount + freeVariablesCount + 2;
                solution[offsetI][offsetj] = system[i][j];
            }
        }
        return solution;
    }

    private void ProduceSolving()
    {
        for (var rowIndex = 0; rowIndex < system.OriginRowsCount; rowIndex++)
            TransformRow(rowIndex);

        if (!system.IsColumnZeroed(system.OriginColumnsCount - 1))
            throw new SolutionException("Last column contains non zero elements");
    }

    private void SimplifySolution()
    {

    }

    private bool NeedToSwap(int rowNumber)
    {
        var skipCount = rowNumber < system.ColumnCount - 1 ? rowNumber + 1 : system.ColumnCount - 1;
        return system[rowNumber].Skip(skipCount).Any(val => val != 0);
    }

    private int GetFreeVariablesCount()
    {
        var count = 0;

        for (int i = 0; i < system.ColumnCount - 1; i++)
            if (system.IsColumnZeroed(i))
                count++;

        return count;
    }


    private void TransformRow(int rowNumber)
    {
        var minValueIndex = 0;
        var count = rowNumber < system.ColumnCount - 1 ? rowNumber : system.ColumnCount - 2;

        while (!system.IsRowTransformed(rowNumber, count))
        {
            (minValueIndex, var isFound) = system.Min(rowNumber, count);

            if (!isFound)
                throw new SolutionException($"Min value not found at row: {rowNumber} | " + string.Join(' ', system[rowNumber]));

            Subtract(rowNumber, minValueIndex);
        }

        if (NeedToSwap(minValueIndex))
            system.SwapColumns(rowNumber, minValueIndex);
    }

    private void Subtract(int rowNumber, int minValueIndex)
    {
        var minuendColumnIndex = GetMinuend(rowNumber, minValueIndex);
        SubtractColumns(minuendColumnIndex, minValueIndex, rowNumber);
    }

    private int GetMinuend(int rowNumber, int minValueIndex)
    {
        for (var i = rowNumber; i < system[rowNumber].Length; i++)
            if (system[rowNumber][i] != 0 && i != minValueIndex)
                return i;

        throw new SolutionException("Can't find minuend");
    }

    private void SubtractColumns(int minuendColumnIndex, int subtrahendColumnIndex, int rowNumber)
    {
        var coef = system[rowNumber][minuendColumnIndex] / system[rowNumber][subtrahendColumnIndex];

        if (coef == 0)
            throw new SolutionException("Coef is zero");

        for (var i = 0; i < system.RowCount; i++)
            system[i][minuendColumnIndex] -= system[i][subtrahendColumnIndex] * coef;
    }
}