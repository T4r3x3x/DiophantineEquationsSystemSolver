namespace DiophantineEquationsSystemSolverCSharp.Solver;

public record Solution
{
    private Solution() { }

    private Solution(int freeMemberShipsCount, int[][] solutions)
    {
        SolutionResult = ESolutionResult.Solved;
        FreeMemberShipsCount = freeMemberShipsCount;
        Solutions = solutions;
    }

    public static Solution FromException(Exception exception) => new Solution
    {
        SolutionResult = ESolutionResult.Error,
        ErrorMessage = exception.Message,
    };

    public static Solution NoSolution(string errorMessage) => new Solution
    {
        SolutionResult = ESolutionResult.NoSolution,
        ErrorMessage = errorMessage
    };

    public static Solution CreateSolved(int freeMemberShipsCount, int[][] solutions) => new Solution(freeMemberShipsCount, solutions);

    public ESolutionResult SolutionResult { get; init; }

    public string? ErrorMessage { get; init; }

    public int FreeMemberShipsCount { get; }

    public int[][]? Solutions { get; }
}