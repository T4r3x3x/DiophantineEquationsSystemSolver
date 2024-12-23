using DiophantineEquationsSystemSolverCSharp.Solver;

using System.Diagnostics;
using System.Text;

namespace DiophantineEquationsSystemSolverCSharp.IO;

public class OutputService
{
    private const string NoSolutionLabel = "NO SOLUTIONS";

    public void WriteSolution(Solution solution)
    {
        var message = solution.SolutionResult switch
        {
            ESolutionResult.Solved => GetSolvedMessage(solution),
            ESolutionResult.NoSolution => NoSolutionLabel,
            ESolutionResult.Error => GetErrorMessage(solution),
            _ => throw new UnreachableException()
        };

        Console.WriteLine(message);
    }

    private static string GetSolvedMessage(Solution solution)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(solution.FreeMemberShipsCount.ToString());

        foreach (var variableSolution in solution.Solutions!)
        {
            stringBuilder.AppendJoin(" ", variableSolution);
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

    private static string GetErrorMessage(Solution solution)
    {
#if DEBUG
        return solution.ErrorMessage!;
#endif
        return string.Empty;
    }
}